using BuildingBlocks.CQRS.Command;
using Catalog.API.Models.Catalogs;

using Marten;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string name, List<string> categories,
        string description, string imageFile, decimal price)
    : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid id);

internal sealed class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand,
    CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        Product newProduct = new Product()
        {
            Id = new Guid(),
            Name = request.name,
            Categories = request.categories,
            Description = request.description,
            ImageFile = request.imageFile,
            Price = request.price
        };

        session.Store(newProduct);
        await session.SaveChangesAsync();

        return new CreateProductResult(newProduct.Id);
    }
}
