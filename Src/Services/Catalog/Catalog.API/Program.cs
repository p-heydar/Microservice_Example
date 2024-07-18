using BuildingBlocks.Behaviors;

using FluentValidation;

using Marten;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;
using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);

#region MediatR
builder.Services
    .AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehaviors<,>));
});
#endregion

#region Api Configration
builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new() { Title = "Eshop Contact Api", Version = "1" });
});
#endregion



#region DataBase Configuration

// Config Postgres From Configration file
builder.Services
    .AddMarten(configuration =>
{
    configuration.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();

// Init Db
builder.Services.InitializeMartenWith<CatalogInitialData>();

// Health Check
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DataBase")!);

#endregion


#region Validator Behavior Configration
builder.Services
    .AddValidatorsFromAssembly(typeof(Program).Assembly);
#endregion

#region Carter Configration
builder.Services
    .AddCarter();
#endregion

var app = builder.Build();

app.MapCarter();

#region Exception Handling
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
#endregion

#region Swagger Configration
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint(
    "/swagger/v1/swagger.json",
    "V1"));
#endregion

#region HealthCeck For Api
app.MapHealthChecks("/health");
#endregion

app.Run();
