
namespace Catalog.API.Products.CreateProducts;

public record CreateProductRequest(string Name, List<string> Catelog, string Description, string ImageFile, decimal Price);
public record CreateProductResponse(Guid Id);



public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
          
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();


            return response;
        });


    }
}