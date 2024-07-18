using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;

public sealed record GetBasketQuery(string userName) : IQuery<GetBasketResult>;

public sealed record GetBasketResult(ShoppingCart Cart);
    
public class GetBasketQueryHandler(IBasketRepository basketRepository):IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasketAsync(query.userName,cancellationToken);
        return new GetBasketResult(basket);
    }
}