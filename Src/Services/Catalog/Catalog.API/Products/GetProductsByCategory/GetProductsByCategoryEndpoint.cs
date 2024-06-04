using Catalog.API.Models.Catalogs;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Immutable;

namespace Catalog.API.Products.GetProductsByCategory;

public sealed record GetProductsByCategoryResponse(IImmutableList<Product> Products);

internal sealed class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Products/Category", async ([FromRoute] string name, ISender sender) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(name));

            var response = result.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithDisplayName("GetProductsByCategory")
        .WithSummary("Get Products By Category")
        .Produces<GetProductsByCategoryResponse>(statusCode: StatusCodes.Status200OK)
        .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
        .WithDescription("Get Products By Category");
    }
}
