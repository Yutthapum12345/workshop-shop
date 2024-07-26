using Basket.API.Data;
using FluentValidation;

namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommad(string userName):ICommand<DeletBasketResult>;


public  record DeletBasketResult(bool IsSuccess);


public class DeleteBasketCommandValidator :AbstractValidator<DeleteBasketCommad>
{

    public DeleteBasketCommandValidator()
    {
        RuleFor(x=>x.userName).NotEmpty().WithMessage("User Not Empty");
    }
}

public class DeleteBasketHandler(IBasketRepository basketRepository):ICommandHandler<DeleteBasketCommad,DeletBasketResult>
{
    
   

    public async Task<DeletBasketResult> Handle(DeleteBasketCommad request, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasketAysnc(request.userName,cancellationToken);
        return new DeletBasketResult(true);
        
    }
}
