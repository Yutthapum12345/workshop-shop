
namespace Catalog.API.Products.UpdateProducts;

public record UpdateProductRequest(string Name, List<string> Catelog, string Description, string ImageFile, decimal Price);
public record UpdateProductResponse(string Name);
public record UpdateProductRequestId(string Name);


public class UpdateProducts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{Name}", async (string Name,UpdateProductRequest request, ISender sender) =>
        {
                
                var command = new UpdateProductRequestId(Name); 
                var reuslt = command.Adapt<UpdateProductICommandId>();

                var result = await sender.Send(reuslt);

                var response = result.Adapt<UpdateProductResponse>();
                
                if(response.Name.Length>0){

                    var reusultcom = request.Adapt<UpdateProductICommand>();

                    var responsecom = await sender.Send(reusultcom);
 
                    var responsee = responsecom.Adapt<UpdateProductResponse>();

                }                
            return response.Name;
        });
                
    }    

}




