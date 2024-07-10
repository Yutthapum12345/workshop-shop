

using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? Page=1,int? PageSize=12,string Cateloo="") : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products,long TotalData =0);
public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async  Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
    
         var products = await session.Query<Product>().Where(p=>p.Catelog.Contains(request.Cateloo)||string.IsNullOrEmpty(request.Cateloo)).ToPagedListAsync(request.Page?? 1,request.PageSize?? 12);

        return new GetProductsResult(products,products.TotalItemCount);

    }
}