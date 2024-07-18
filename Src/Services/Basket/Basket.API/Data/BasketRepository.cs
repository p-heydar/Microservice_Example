using Basket.API.Exceptions;
using Marten;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session):IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
    {
        var findBasket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken)??throw new BasketNotFoundException(userName);
        return findBasket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);

        return cart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}