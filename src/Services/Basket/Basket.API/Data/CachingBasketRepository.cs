using System.Text.Json;
using Basket.API.Exeptions;
using Basket.API.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachingBasketRepository(IBasketRepository basketRepository,IDistributedCache cache) : IBasketRepository
{
   
    public async Task<ShoppingCart> StoreBasketAsync( ShoppingCart basket, CancellationToken cancellationToken = default)
    {
       await basketRepository.StoreBasketAsync(basket,cancellationToken);
       await cache.SetStringAsync(basket.UserName,JsonSerializer.Serialize(basket),cancellationToken);
       return basket;
    }

    public async Task<bool> DeleteBasketAysnc(string userName ,CancellationToken cancellationToken = default)
    {
       await basketRepository.DeleteBasketAysnc(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        return true;
    }

    public  async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(userName,cancellationToken);

        if(cacheBasket is not null)
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
        }
        var basket = await basketRepository.GetBasketAsync(userName,cancellationToken);
        await cache.SetStringAsync(userName,JsonSerializer.Serialize(basket),cancellationToken);

        return basket;
    }
}
