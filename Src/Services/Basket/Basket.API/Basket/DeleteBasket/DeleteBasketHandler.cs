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

public sealed class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        return new DeleteBasketResult(true);
    }
}