
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductRequest(string Name, List<string> Categories,
        string Description, string ImageFile, decimal Price);

public sealed record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Products",
            async (CreateProductRequest request, ISender sender)
            =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/Products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(statusCode: StatusCodes.Status201Created)
        .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
