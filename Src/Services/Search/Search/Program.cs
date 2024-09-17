using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSetting>(builder.Configuration);

builder.Services.AddScoped(service =>
{
    var appSetting = service.GetRequiredService<IOptions<AppSetting>>().Value.ElasticSearchConfiguration;
    var settings = new ElasticsearchClientSettings(new Uri(appSetting.Host))
        .CertificateFingerprint(appSetting.Fingerprint)
        .Authentication(new BasicAuthentication(appSetting.UserName, appSetting.Password));
    
    var client = new ElasticsearchClient(settings);
    // return client;
    return client;
});




var app = builder.Build();

app.MapPost("/", async (ElasticsearchClient client, string newValue) =>
{
    var test = new test(newValue);
    var result = await client.IndexAsync(test, index: "my-index");

    if (result.IsValidResponse)
        return Results.Ok("True");
    else
        return Results.Ok("False");

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();


public sealed record test(string text);

public sealed class AppSetting
{
    public ElasticSearchConfiguration ElasticSearchConfiguration { get; set; }
}

public sealed class ElasticSearchConfiguration
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Fingerprint { get; set; }
}