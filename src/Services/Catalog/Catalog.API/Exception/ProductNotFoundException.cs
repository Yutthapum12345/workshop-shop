namespace Catalog.API;

public class ProductNotFoundException :Exception
{

    public ProductNotFoundException(Guid Id):base($"Product with id {Id} was  ")
    {

        
    }

}
