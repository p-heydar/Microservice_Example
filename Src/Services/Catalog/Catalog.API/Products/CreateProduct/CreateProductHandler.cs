using BuildingBlocks.CQRS.Command;
using Catalog.API.Models.Catalogs;

using FluentValidation;

using Marten;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories,
        string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name Is Required");

        RuleFor(x => x.Categories)
            .NotEmpty()
            .WithMessage("Category Is Required");

        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("Image Is Required");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price Must Be GreaterThan 0");
    }
}

public sealed record CreateProductResult(Guid Id);

internal sealed class CreateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand,
    CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        Product newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        session.Store(newProduct);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(newProduct.Id);
    }
}
