namespace BuildingBlocks.Messaging.Events;

public sealed record ProductCreated:IntegrationEvent
{
    public string Name { get; set; } = default!;
    public List<string> Categories { get; set; } = new();
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = decimal.Zero;
    public ProductImageData file { get; set; }
};

public sealed record ProductImageData(string file, string fileName, string contentType, long fileSize);