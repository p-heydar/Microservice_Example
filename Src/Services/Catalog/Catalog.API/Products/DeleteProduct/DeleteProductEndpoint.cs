
using Marten;

namespace Catalog.API.Products.DeleteProduct;


public sealed record DeleteProductResponse(bool IsSuccess);

public sealed class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Product/{Id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(Id));

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .WithDescription("DeleteProduct By Id")
        .WithSummary("Delete Product")
        .Produces<DeleteProductResponse>(statusCode: StatusCodes.Status200OK)
        .ProducesProblem(statusCode: StatusCodes.Status400BadRequest);
    }
}
