using Basket.API.Models;

namespace Basket.API.Data;

public interface IBasketRepository
{

    Task<ShoppingCart>GetBasketAsync(string userName,CancellationToken cancellationToken =default);


    Task<ShoppingCart>StoreBasketAsync(ShoppingCart basket,CancellationToken cancellationToken =default);

    Task<bool>DeleteBasketAysnc(string userName ,CancellationToken cancellationToken=default);
  
}
