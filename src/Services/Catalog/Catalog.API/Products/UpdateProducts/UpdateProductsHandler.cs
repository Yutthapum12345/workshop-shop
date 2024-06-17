

namespace Catalog.API.Products.UpdateProducts;





public record UpdateProductICommand(string Name, List<string> Catelog, string Description, string ImageFile, decimal Price):ICommand<UpdateProductResult>;
public record UpdateProductICommandId(string Name):ICommand<UpdateProductResult>;
public record UpdateProductResult(string Name);



public class UpdateProductICommandHandler(IDocumentSession session):ICommandHandler<UpdateProductICommandId,UpdateProductResult>
{

    public async Task<UpdateProductResult>Handle(UpdateProductICommandId request ,CancellationToken cancellationToken)
    {

        var products  = await session.Query<Product>().FirstOrDefaultAsync(pd=>pd.Name==request.Name);

        if(products.Name.Length==0){

           throw new ProductNotFoundException(products.Id);

        } 

       return new UpdateProductResult(products.Name);


    }

};

public class UpdateProductIdICommandHandler(IDocumentSession session):ICommandHandler<UpdateProductICommand,UpdateProductResult>
{


        public async Task<UpdateProductResult>Handle(UpdateProductICommand request ,CancellationToken cancellationToken)
        {
            var products  = await session.Query<Product>().FirstOrDefaultAsync(pd=>pd.Name==request.Name);
            

           
            products.Catelog = request.Catelog;
            products.Description = request.Description;
            products.ImageFile = request.ImageFile;
            products.Price = request.Price;

        

            session.Update(products);
            await session.SaveChangesAsync();

            
            return new UpdateProductResult(products.Name);
            


        }
       







  
};





