

using Marten.Pagination;

namespace Catalog.API.Products.GetLastProduct;

public record GetLastProductQuery() : IQuery<GetLastProductResult>;
public record GetLastProductResult(Product LastProduct);
public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetLastProductQuery, GetLastProductResult>
{
    public async  Task<GetLastProductResult> Handle(GetLastProductQuery request, CancellationToken cancellationToken)
    {     
      
        var total = await session.Query<Product>().CountAsync();
        
        var lastProduct = await session.Query<Product>().Skip(total-1).FirstOrDefaultAsync();

        return new GetLastProductResult(lastProduct!);
   
       
    }
}