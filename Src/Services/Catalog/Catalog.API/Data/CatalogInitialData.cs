using System.Collections.Immutable;
using Catalog.API.Models.Catalogs;
using Marten;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData:IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;
        session.Store(GetPreConfigureProducts());
        await session.SaveChangesAsync(cancellation);

        return;
    }

    public static IEnumerable<Product> GetPreConfigureProducts()
    {
        List<Product> products = new();

        Product product1 = new Product()
        {
            Id = new Guid(),
            Description = "Test Product",
            ImageFile = "test",
            InsertDate = DateTime.Now,
            Categories = new List<string>()
            {
                "brand1", "brand2"
            },
            Name = "Product1",
            Price = 10000
        };

        products.Add(product1);

        return products;
    }
}