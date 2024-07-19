using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CacheBasketRepository(IBasketRepository basketRepository, IDistributedCache cache)
    :IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
    {
        #region Cache
        var findFromCache = await cache.GetStringAsync(userName, cancellationToken);
        if (findFromCache is not null)
        {
            return JsonSerializer.Deserialize<ShoppingCart>(findFromCache)!;
        }
        #endregion
        else
        {
            var findFromDb = await basketRepository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(findFromDb), cancellationToken);

            return findFromDb;
        }
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        // Put In Db
        await basketRepository.StoreBasketAsync(cart, cancellationToken);
        
        // Put In Cache 
        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);

        return cart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
    {
        // Delete From Db
        await basketRepository.DeleteBasketAsync(userName, cancellationToken);

        // Remove From Cache
        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}