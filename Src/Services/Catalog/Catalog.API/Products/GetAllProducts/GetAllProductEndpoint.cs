using System.Collections.Immutable;

using Catalog.API.Models.Catalogs;

namespace Catalog.API.Products.GetAllProducts;

public sealed record GetAllProductsResponse(IEnumerable<Product> Products);

public sealed class GetAllProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Products", async (int pageNumber , 
                int pageSize, ISender sender) =>
        {
            var result = await sender.Send(new GetAllProductQuery(pageNumber, pageSize));
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
