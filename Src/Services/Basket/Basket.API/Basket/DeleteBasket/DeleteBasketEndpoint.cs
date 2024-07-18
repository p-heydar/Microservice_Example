using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;

public sealed record DeleteBasketResponse(bool IsSuccess);

public sealed class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        });
    }
}