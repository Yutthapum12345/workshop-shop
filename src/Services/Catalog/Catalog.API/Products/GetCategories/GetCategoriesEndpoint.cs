
namespace Catalog.API.Products.GetLastProduct;

public record GetCategoriesRequest(string Cate) :IQuery<GetCategoriesResponse>;
public record GetCategoriesResponse(List<Product> Products);
public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async ([AsParameters] GetCategoriesRequest request, ISender sender) =>
        {
           
            var query = request.Adapt<GetCategoriesQuery>();

            var response = await sender.Send(query);

            var result = response.Adapt<GetCategoriesResponse>();

            return result;
     

               
        });
    }
}