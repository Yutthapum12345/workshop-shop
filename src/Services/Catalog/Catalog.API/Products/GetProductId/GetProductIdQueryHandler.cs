

namespace Catalog.API.Products.GetProductId;

public record GetProductIdQuery(Guid Id):IQuery<GetProductIdResult>;
public record GetProductIdResult(IEnumerable<Product>Products);
public class GetProductOIdQuery(IDocumentSession session) : IQueryHandler<GetProductIdQuery, GetProductIdResult>
{
    public async  Task<GetProductIdResult> Handle(GetProductIdQuery request, CancellationToken cancellationToken)
    {

        var products = await session.Query<Product>().FirstOrDefaultAsync(pd=>pd.Id==request.Id);
        
       

        if(products==null)
        {
            throw new ProductNotFoundException(products.Id);
        }

        return new GetProductIdResult(new List<Product>{products});

       
    }   

           
    };
