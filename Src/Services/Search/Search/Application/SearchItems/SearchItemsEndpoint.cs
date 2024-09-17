using Carter;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Search.Models;

namespace Search.Application.SearchItems;

public sealed class SearchItemsEndpoint:ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/SearchItems", async (string Query, ElasticsearchClient _elasticSearchClient) =>
        {
            var response = await _elasticSearchClient.SearchAsync<CatalogItemIndex>(search =>
                search.Index(CatalogItemIndex.IndexName)
                    .From(0)
                    .Size(100)
                    .Query(query =>
                        query.Term(term => term.Field(field => field.Name)
                            .Value(Query))));
            if (response.IsValidResponse)
                return Results.Ok(response.Documents);
            return Results.NotFound();
        });
    }
}