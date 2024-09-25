using System.Net;
using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Media;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region MessageBroker

builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

#endregion

#region Carter Configration
builder.Services
    .AddCarter();
#endregion

#region MinIo

// builder.Services.AddMinIoClient(builder.Configuration);

var endpoint = builder.Configuration["MinIo:MinioEndpoint"];
var accessKey = builder.Configuration["MinIo:AccessKey"];
var secretKey = builder.Configuration["MinIo:SecretKey"];

builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(endpoint)
    .WithCredentials(accessKey, secretKey)
    .WithSSL(false)
    .Build());

#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();


app.MapPost("/test",async (string bucketName, IMinioClient minioClient) =>
{
    var find = minioClient.ListBucketsAsync().Result;
    return Results.Ok(find.Buckets);

}).DisableAntiforgery();

app.Run();
