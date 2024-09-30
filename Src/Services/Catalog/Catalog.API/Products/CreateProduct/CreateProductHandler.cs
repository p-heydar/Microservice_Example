using System.Buffers.Text;
using BuildingBlocks.CQRS.Command;
using BuildingBlocks.Messaging.Events;
using Catalog.API.Models.Catalogs;

using FluentValidation;

using Marten;
using MassTransit;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Categories,
        string Description, IFormFile ImageFile, decimal Price)
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

internal sealed class CreateProductCommandHandler(IDocumentSession session, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateProductCommand,
    CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        Product newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            Price = command.Price
        };

        session.Store(newProduct);
        await session.SaveChangesAsync(cancellationToken);


        byte[] bytes;
        using (var memoryStream = new MemoryStream())
        {
            await command.ImageFile.CopyToAsync(memoryStream);
            bytes = memoryStream.ToArray();
        }

        var base64 = Convert.ToBase64String(bytes);
        
        await publishEndpoint.Publish(new ProductCreated
        {
            Name = newProduct.Name,
            Categories = newProduct.Categories,
            Description = newProduct.Description,
            Price = newProduct.Price,
            file = new ProductImageData(file: base64,
                fileName: command.ImageFile.FileName,
                contentType: command.ImageFile.ContentType,
                fileSize: command.ImageFile.Length)
        });
        
        
        
        return new CreateProductResult(newProduct.Id);
    }
}
