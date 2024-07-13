namespace Basket.API.Basket.GetBasket;

public sealed record GetBasketQuery(string userName) : IQuery<GetBasketResult>;

public sealed record GetBasketResult(ShoppingCart Cart);
    
public class GetBasketQueryHandler:IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        return new(new ShoppingCart("test"));
    }
}