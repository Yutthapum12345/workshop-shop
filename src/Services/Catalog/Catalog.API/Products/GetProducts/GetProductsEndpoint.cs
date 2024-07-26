

namespace Catalog.API.Products.GetProducts;
public record GetProductsRequest(int? Page=1,int? PageSize=12,string? Cateloo="") :IQuery<GetProductsResponse>;
public record GetProductsResponse(IEnumerable<Product> Products,long TotalData =0);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
           
            var query = request.Adapt<GetProductsQuery>();
            var response = await sender.Send(query);
            var result = response.Adapt<GetProductsResponse>();

            return result;
        });
    }
}