using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Search.Infrastructure;
using Search.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

#region Api

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Elastic

builder.Services.Configure<AppSetting>(builder.Configuration);

builder.Services.AddElasticSearch();

#endregion

#region MessageBroker

builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

#endregion

#region Carter Configration
builder.Services
    .AddCarter();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();