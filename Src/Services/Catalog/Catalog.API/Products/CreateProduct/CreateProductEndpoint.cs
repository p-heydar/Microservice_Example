
namespace Catalog.API.Products.CreateProduct;

public sealed record CreateProductRequest(string name, List<string> categories,
        string description, string imageFile, decimal price);

public sealed record CreateProductResponse(Guid id);

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

            return Results.Created($"/Products/{response.id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(statusCode: StatusCodes.Status201Created)
        .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
