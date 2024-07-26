using System.Windows.Input;
using Basket.API.Data;
using Basket.API.Models;
using FluentValidation;
using JasperFx.CodeGeneration.Frames;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);


public class StoreBasketCommandValidator :AbstractValidator<StoreBasketCommand>
{

    public StoreBasketCommandValidator()
    {
        RuleFor(x=>x.Cart).NotNull().WithMessage("Cart Not null");
    }
}

public class StoreBasketHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult>Handle(StoreBasketCommand request,CancellationToken cancellationToken)
    {
            ShoppingCart cart = request.Cart;
            await basketRepository.StoreBasketAsync(cart,cancellationToken);
            return new StoreBasketResult(cart.UserName);
    }

}
