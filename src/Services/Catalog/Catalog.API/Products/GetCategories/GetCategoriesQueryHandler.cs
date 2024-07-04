

using Marten.Pagination;

namespace Catalog.API.Products.GetLastProduct;

public record GetCategoriesQuery(string Cate) : IQuery<GetCategoriesResult>;
public record GetCategoriesResult(List<Product>Products);
public class GetCategoriesQueryHandler(IDocumentSession session) : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async  Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = session.Query<Product>().Where(p => p.Catelog.Contains(request.Cate)).ToList();

            // You may adjust this based on how you want to paginate the results
           
                                    
        return  new  GetCategoriesResult(query);
           
    }
}