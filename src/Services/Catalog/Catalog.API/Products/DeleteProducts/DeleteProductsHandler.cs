namespace Catalog.API.Products.DeleteProducts;

public record DeleteProductICommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);



public class DeleteProductsHandler(IDocumentSession session) : ICommandHandler<DeleteProductICommand, DeleteProductResult>
{

    public async Task<DeleteProductResult> Handle(DeleteProductICommand request, CancellationToken cancellationToken)
    {

        var product = await session.LoadAsync<Product>(request.Id);

        
        if (product == null)
        {
            throw new Exception("Not Found");
        }




        session.Delete(product);
        await session.SaveChangesAsync();

        return new DeleteProductResult(true);

    }
}
