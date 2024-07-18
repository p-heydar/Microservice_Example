using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public sealed record StoreBasketResult(ShoppingCart Cart);

public sealed class StockBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StockBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart Cannot Be Null;");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName Is Requierd");
    }
}

public sealed class StoreBasketCommandHandler(IBasketRepository basketRepository) :
    ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);
        return new StoreBasketResult(basket);
    }
}