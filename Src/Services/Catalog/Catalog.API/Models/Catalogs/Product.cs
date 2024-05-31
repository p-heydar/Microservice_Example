using Catalog.API.Models.Common;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.API.Models.Catalogs;

public class Product:BaseEntity<Guid>
{
    public string Name { get; set; } = default!;
    public List<string> Categories { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; } = decimal.Zero;
}
