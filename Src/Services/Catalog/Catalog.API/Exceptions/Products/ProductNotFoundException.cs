
using BuildingBlocks.Exceptions;
using Catalog.API.Models.Catalogs;

namespace Catalog.API.Exceptions.Products;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid key) : base(nameof(Product), key)
    {
    }
}
