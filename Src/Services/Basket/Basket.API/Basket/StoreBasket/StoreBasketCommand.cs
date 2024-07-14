namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public sealed record StoreBasketResult(bool IsSuccess);

public sealed class StockBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StockBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart Cannot Be Null;");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName Is Requierd");
    }
}

public sealed class StoreBasketCommandHandler :
    ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        return new StoreBasketResult(true);
    }
}