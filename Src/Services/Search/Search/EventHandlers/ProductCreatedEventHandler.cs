using BuildingBlocks.Messaging.Events;
using Elastic.Clients.Elasticsearch;
using MassTransit;
using Search.Models;

namespace Search.EventHandlers;

public sealed class ProductCreatedEventHandler(ElasticsearchClient _elasticsearchClient):IConsumer<ProductCreated>
{
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        var message = context.Message;
        if (message is null)
            return;

        var newItemIndex = new CatalogIndexItem
        {
            Name = message.Name,
            Categories = message.Categories,
            Description = message.Description,
            Price = message.Price,
            ImageFile = message.ImageFile,
            Id = message.Id,
            EventType = message.EventType,
            OccurredOn = message.OccurredOn
        };

        var result = await _elasticsearchClient.Indices.ExistsAsync(CatalogIndexItem.IndexName);
        
        if (!result.Exists)
            await _elasticsearchClient.Indices.CreateAsync<CatalogIndexItem>(index: CatalogIndexItem.IndexName);

        var addProductIndexItem =
            await _elasticsearchClient.IndexAsync(index: CatalogIndexItem.IndexName, document: newItemIndex);

        var results = addProductIndexItem.IsValidResponse;
       return;
    }
}