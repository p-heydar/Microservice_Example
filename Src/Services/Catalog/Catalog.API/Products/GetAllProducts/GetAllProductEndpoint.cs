using System.Collections.Immutable;

using Catalog.API.Models.Catalogs;

namespace Catalog.API.Products.GetAllProducts;

public sealed record GetAllProductsResponse(IImmutableList<Product> Products);

internal sealed class GetAllProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllProductQuery());
            var response = result.Adapt<GetAllProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetAllProducts")
        .WithDescription("GetAllProducts")
        .Produces<GetAllProductsResponse>(statusCode: StatusCodes.Status200OK)
        .ProducesProblem(statusCode: StatusCodes.Status500InternalServerError)
        .WithSummary("GetAllProducts");
    }
}
