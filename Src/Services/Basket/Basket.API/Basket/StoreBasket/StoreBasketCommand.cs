using Basket.API.Data;

using Discount.Grpc;

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

public sealed class StoreBasketCommandHandler(IBasketRepository basketRepository,
    DiscountProtoService.DiscountProtoServiceClient discountProto) :
    ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        var basket = await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);
        return new StoreBasketResult(basket);
    }

   private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var findCoupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= findCoupon.Amount;
        }
    }
}