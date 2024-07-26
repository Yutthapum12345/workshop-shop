using Discout.Grpc.Models;
using Microsoft.EntityFrameworkCore;
namespace Discout.Grpc.Data;

public class DiscountConext:DbContext
{

    public DiscountConext(DbContextOptions<DiscountConext>options):base (options){}

    public DbSet<Coupon> Coupons {get;set;}

    


}
