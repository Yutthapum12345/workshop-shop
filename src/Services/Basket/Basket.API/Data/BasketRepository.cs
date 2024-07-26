﻿using Basket.API.Exeptions;
using Basket.API.Models;
using Marten;

namespace Basket.API.Data;
public class BasketRepository(IDocumentSession session) : IBasketRepository
{

     public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(userName,cancellationToken);
        return basket is null ? throw new BasketNotFoundExeption(userName):basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
       session.Store(basket);
       await session.SaveChangesAsync(cancellationToken);
       return basket;
    }
    public async Task<bool> DeleteBasketAysnc(string userName, CancellationToken cancellationToken = default)
    {
       session.Delete<ShoppingCart>(userName);

       await session.SaveChangesAsync(cancellationToken);
       return  true;
    }


    
}
