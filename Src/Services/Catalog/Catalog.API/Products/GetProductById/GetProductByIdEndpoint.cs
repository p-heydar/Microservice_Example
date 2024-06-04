
using Catalog.API.Models.Catalogs;

namespace Catalog.API.Products.GetProductById;

public  record GetProductByIdResponse(Product Product);

public sealed class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("$/Products/{Id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
        .WithDisplayName("GetProductById")
        .WithSummary("Get Product By Id")
        .Produces<GetProductByIdResponse>(statusCode: StatusCodes.Status200OK)
        .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
        .WithDescription("Get Product By Id");
    }
}
