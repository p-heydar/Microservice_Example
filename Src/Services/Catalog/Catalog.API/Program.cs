using BuildingBlocks.Behaviors;

using FluentValidation;

using Marten;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;
using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehaviors<,>));
});


builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new() { Title = "Eshop Contact Api", Version = "1" });
});

builder.Services
    .AddMarten(configuration =>
{
    configuration.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();

builder.Services
    .AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.InitializeMartenWith<CatalogInitialData>();


builder.Services
    .AddCarter();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DataBase")!);

var app = builder.Build();
app.MapCarter();

if (app.Environment.IsDevelopment())
{

    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is null)
            {
                return;
            }

            ProblemDetails problemDetails = new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.StackTrace,
                Status = StatusCodes.Status500InternalServerError,
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problemDetails);
        });
    });


}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint(
    "/swagger/v1/swagger.json",
    "V1"));

app.MapHealthChecks("/health");

app.Run();
