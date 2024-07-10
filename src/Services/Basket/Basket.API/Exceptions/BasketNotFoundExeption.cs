using BuildingBlocks.Exceptions;

namespace Basket.API.Exeptions;

public class BasketNotFoundExeption : NotFoundException
{
    public BasketNotFoundExeption(string userName) : base($"Basket not found for user{userName}")
    {
    }
}
