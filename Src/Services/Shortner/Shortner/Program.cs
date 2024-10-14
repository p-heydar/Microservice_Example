using System.Reflection;
using BuildingBlocks.Behaviors;
using Carter;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shortner.Data;
using Shortner.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Carter

builder.Services.AddCarter();

#endregion

#region MediatR

builder.Services.AddMediatR(configuration: configuration =>
{
    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    configuration.AddOpenBehavior(typeof(ValidationBehaviors<,>));

});

#endregion

#region Database

string connectionString = builder.Configuration.GetConnectionString("Mongo")!;
string dataBaseName = builder.Configuration.GetSection("DatabaseName").Value!;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMongoDB(connectionString, dataBaseName));

#endregion


#region Validator

builder.Services
    .AddValidatorsFromAssembly(typeof(Program).Assembly);

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async (AppDbContext _dbContext) =>
{
    var res = await _dbContext.Urls.AsNoTracking().ToListAsync();
    return Results.Ok(res);
});

app.UseHttpsRedirection();

app.MapCarter();


app.Run();
