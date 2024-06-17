namespace Catalog.API.Products.GetProductsByCategory;
public record GetProductsByCategoryQuery(string Cate):IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product>Products);
public class GetProductsByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{

  public async  Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {

           var product = await session.Query<Product>().Where(pd=>pd.Catelog.Contains(request.Cate)).ToListAsync();
        return new GetProductsByCategoryResult(product);
           
    }


}
