namespace Catalog.API.Products.GetProductsByCategory;
public record GetProductIdRequest(string Cate);
public record GetProductIdResponse(IEnumerable<Product>Products);
public class GetProductsByCategoryEndpoint
{
   public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/Category/{Cate}", async (string Cate,ISender sender) =>
        {
           
            var query = new GetProductIdRequest(Cate);
           ;


            var result = await sender.Send(query);
            var response = result.Adapt<GetProductIdResponse>();

            return response;
     

               
        });
    }



}
