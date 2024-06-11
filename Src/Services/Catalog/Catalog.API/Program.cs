using BuildingBlocks.Behaviors;

using FluentValidation;

using Marten;

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


builder.Services
    .AddCarter();

var app = builder.Build();
app.MapCarter();



app.UseSwagger();
app.UseSwaggerUI(c=>c.SwaggerEndpoint(
    "/swagger/v1/swagger.json",
    "V1"));


app.Run();
