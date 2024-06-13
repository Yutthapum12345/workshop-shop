

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
public class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return new Task<GetProductsResult>(() => new GetProductsResult(new List<Product>()));
    }
}