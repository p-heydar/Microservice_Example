namespace Catalog.API.Exceptions.Products;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product Was Not Found")
    {
    }
}
