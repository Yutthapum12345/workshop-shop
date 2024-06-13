
namespace Catalog.API.Products.GetProducts;
//public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
           
            var query = new GetProductsQuery();

            var response = await sender.Send(query);

            return new GetProductsResponse(new List<Product>());
        });
    }
}