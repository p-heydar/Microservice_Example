namespace Basket.API.Data;

public interface ICacheBasketRepository
{
    Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken);
    Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken);
    Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken);
}