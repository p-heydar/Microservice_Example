using Catalog.API.Models.Catalogs;
using Catalog.API.Products.GetProductsByCategory;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.UpdateProduct;

public sealed record UpdateProductRequest(Product Product);

public sealed record UpdateProductResponse(bool IsSuccess);

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Please Send Valid Id");
        RuleFor(x => x.Product.Categories).NotNull().WithMessage("Please Send Categories");
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Please Send Valid Name");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Please Send Valid Price");
    }
}

public sealed class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Products", async ([FromBody]UpdateProductRequest request,
            ISender sender) =>
        {
            var convertToCommand = request.Adapt<UpdateProductCommand>();

            var result = await sender.Send(convertToCommand);

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .WithDisplayName("UpdateProduct")
        .WithSummary("Update Product")
        .Produces<UpdateProductResponse>(statusCode: StatusCodes.Status200OK)
        .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
        .WithDescription("Update Product");
    }
}
