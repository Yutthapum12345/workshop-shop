using Discout.Grpc.Protos;
using Grpc.Core;
using Discout.Grpc.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Discout.Grpc.Models;
namespace Discout.Grpc.Service;


public class DiscoutService(DiscountConext discountConext,ILogger<DiscoutService> logger):DiscoutProtoService.DiscoutProtoServiceBase

{
    public override async Task<CouponModel> GetDiscout(GetDiscoutRequest request, ServerCallContext context)
    {
        var coupon = await discountConext.Coupons.FirstOrDefaultAsync(c=>c.ProductName==request.ProductName);
        
        if(coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "No coupon found"));
        }

        logger.LogInformation("");
        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

public override async Task<CouponModel> CreateDiscout(CreateDiscoutRequest request, ServerCallContext context)
{
    // ตรวจสอบว่าข้อมูลคูปองมีอยู่ใน request หรือไม่
    if (request?.Coupon == null)
    {
        throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon data is missing in the request"));
    }

    // แปลงข้อมูลจาก CreateDiscoutRequest ไปเป็น Coupon
    var discoutModel = request.Coupon.Adapt<Coupon>();

    // เพิ่มข้อมูลคูปองลงในฐานข้อมูล
    discountConext.Coupons.Add(discoutModel);
    await discountConext.SaveChangesAsync();
    
    // แปลงข้อมูลจาก Coupon ที่บันทึกลงในฐานข้อมูล ไปเป็น CouponModel
    var couponModel = discoutModel.Adapt<CouponModel>();
    
    // คืนค่าข้อมูลคูปอง
    return couponModel;
}


    public override async Task<DeleteDiscountResponse> DeleteDiscout(DeleteDiscoutRequest request, ServerCallContext context)
    {
        var coupon = await discountConext.Coupons.FirstOrDefaultAsync(x=>x.ProductName==request.ProductName);
        if(coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,"Delete is Failed"));
        }
        discountConext.Coupons.Remove(coupon);
        await discountConext.SaveChangesAsync();
        return new DeleteDiscountResponse(new DeleteDiscountResponse{Success=true});
    }

    public override async Task<UpdateDiscoutResponse> UpdateDiscout(UpdateDiscoutRequest request, ServerCallContext context)
    {
        var coupon = await discountConext.Coupons.FirstOrDefaultAsync(x=>x.ProductName==request.ProductName);
      

        var discoutModel = coupon.Adapt<Coupon>();
        if(coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,"Delete is Failed"));
        }
        discountConext.Coupons.Update(coupon);
        await discountConext.SaveChangesAsync();

        var couponModel = discoutModel.Adapt<CouponModel>();

        return new UpdateDiscoutResponse(new UpdateDiscoutResponse{Coupon=couponModel});
       
    }




}
