using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.CQRS;

namespace Basket.API.Basket.GetBasket;

public record GetBasketRequest(string userName):IQuery<GetBasketResult>;


public record GetBasketResult(ShoppingCart Cart);




public class GetBasketHandler(IBasketRepository basketRepository):IQueryHandler<GetBasketRequest,GetBasketResult>
{
    public async Task<GetBasketResult>Handle(GetBasketRequest request, CancellationToken cancellationToken)
    {

      var cart= await basketRepository.GetBasketAsync(request.userName,cancellationToken);
      return new GetBasketResult(cart);
    }

}
