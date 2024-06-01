namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductRequest(string name, List<string> categories,
        string description, string imageFile, decimal price);

public sealed record CreateProductResponse(Guid id);

public class CreateProductEndpoint
{
}
