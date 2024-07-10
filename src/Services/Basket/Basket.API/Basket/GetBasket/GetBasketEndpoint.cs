using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;
using OpenTelemetry.Trace;

namespace Basket.API.Basket.GetBasket;

// public record  GetBaskQuery(string UserName);

public record GetBaseResponse(ShoppingCart Cart);

public class GetBasketEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGet("/basket/{userName}",async (string userName,ISender sender)=>
        {

                var result=  await sender.Send(new GetBasketRequest(userName));

                var response = result.Adapt<GetBaseResponse>();

                return response;
        }).Produces<GetBaseResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetBasket")
        .WithDescription("Get the shopping cart for a user ");
    }

}
