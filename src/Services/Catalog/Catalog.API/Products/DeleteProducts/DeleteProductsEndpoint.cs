

namespace Catalog.API.Products.DeleteProducts // Adjust the namespace as per your project structure
{
    

    public record DeleteResponse(bool IsSuccess);

    public class DeleteProducts : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
            {
                

                var command = new DeleteProductICommand(Id);

                var result = await sender.Send(command); 

                var response = result.Adapt<DeleteResponse>(); 

                return Results.Ok(response);
            });
        }
    }


}
