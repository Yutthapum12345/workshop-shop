


namespace Catalog.API.Products.GetLastProduct;
public record GetLastProductRequest() :IQuery<GetLastProductResponse>;
public record GetLastProductResponse(Product LastProduct);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/lastproduct", async ( ISender sender) =>
        {
            var query = new GetLastProductQuery();

            var response = await sender.Send(query);

            var result = response.Adapt<GetLastProductResponse>();

            return result; 
        });
    }
}