using BuildingBlocks.CQRS.Command;
using Catalog.API.Models.Catalogs;

using Marten;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories,
        string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

internal sealed class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand,
    CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        Product newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Categories = request.Categories,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };

        session.Store(newProduct);
        await session.SaveChangesAsync();

        return new CreateProductResult(newProduct.Id);
    }
}
