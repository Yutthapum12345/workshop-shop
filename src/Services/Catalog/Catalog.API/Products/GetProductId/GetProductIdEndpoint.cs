
namespace Catalog.API.Products.GetProductId;


public record GetProductIdRequest(Guid Id);
public record GetProductIdResponse(IEnumerable<Product>Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{Id}", async (Guid Id,ISender sender) =>
        {
           
            var query = new GetProductIdRequest(Id);
            var reuslt = query.Adapt<GetProductIdQuery>();


            var result = await sender.Send(reuslt);

            var response = result.Adapt<GetProductIdResponse>();

            return response;
     

               
        });
    }
}