using Marten;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});


builder.Services.AddMarten(configuration =>
{
    configuration.Connection(builder.Configuration.GetConnectionString("DataBase")!);
}).UseLightweightSessions();
var app = builder.Build();

app.MapCarter();

app.Run();
