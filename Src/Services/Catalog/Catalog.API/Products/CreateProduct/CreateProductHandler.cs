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
            .WithMessage("Category Is Requierd");

        RuleFor(x => x.ImageFile)
            .NotEmpty()
            .WithMessage("Image Is Requierd");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price Must Be GreaterThan 0");
    }
}

public sealed record CreateProductResult(Guid Id);

internal sealed class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
    : ICommandHandler<CreateProductCommand,
    CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        //var validation = await validator
        //    .ValidateAsync(command, cancellationToken);

        //var errors = validation.Errors
        //    .Select(x => x.ErrorMessage)
        //    .ToList();
        //if (errors.Any())
        //{
        //    string errorsText = default;
        //    foreach (var item in errors)
        //    {
        //        errorsText += item + Environment.NewLine;
        //    }
        //    throw new ValidationException(errorsText);
        //}

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
        await session.SaveChangesAsync();

        return new CreateProductResult(newProduct.Id);
    }
}
