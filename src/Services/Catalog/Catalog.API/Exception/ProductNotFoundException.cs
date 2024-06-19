using BuildingBlocks.Exceptions;

namespace Catalog.API;

public class ProductNotFoundException :NotFoundException
{

    public ProductNotFoundException(Guid Id)
    :base("Product",Id)
    {

        
    }

}
