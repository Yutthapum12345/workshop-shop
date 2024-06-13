
using Marten;

namespace Catalog.API.Products.CreateProducts;

public record CreateProductCommand(string Name, List<string> Catelog, string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
   

   
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            
            Name= request.Name,
            Catelog = request.Catelog,
            Description = request.Description,
            ImageFile= request.ImageFile,
            Price = request.Price

        };

        session.Store(product);
        await session.SaveChangesAsync();


       
     return new CreateProductResult(product.Id);
    }
};