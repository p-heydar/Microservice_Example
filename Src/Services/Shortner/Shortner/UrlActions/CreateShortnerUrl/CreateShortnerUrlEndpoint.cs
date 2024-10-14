using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shortner.Data;
using Shortner.Models;
using Shortner.UrlActions.CreateShortnerUrl;
using Shortner.Utilities;

namespace Shortner.UrlActions.CreateShortnerUrl;

public class CreateShortnerUrlEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/CreateShortnerUrl", async (ISender sender,[FromQuery] string orginalUrl)=>
        {
            var result = await sender.Send(new CreateShortnerUrlCommand(orginalUrl));
            return Results.Ok(result);
        });
    }
}