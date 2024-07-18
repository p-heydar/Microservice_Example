using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket;

public sealed record DeleteBasketCommand(string Username):ICommand<DeleteBasketResult>;
public sealed record DeleteBasketResult(bool Issucces);

public sealed class DeleteBasketCommandValidator :
    AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("UserName Is Requierd");
    }
}

public sealed class DeleteBasketCommandHandler(IBasketRepository basketRepository) :
    ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.DeleteBasketAsync(command.Username, cancellationToken);
        return new DeleteBasketResult(basket);
    }
}