using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.StoreBasket;

public sealed record StoreBasketRequest(ShoppingCart Cart);

public sealed record StoreBasketResponse(bool Issuccess);

public class StoreBasketEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket{response.Issuccess}", response);
        })
        .WithName("Store Basket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary(" Store Basket")
        .WithDescription("Store Basket");
    }
}