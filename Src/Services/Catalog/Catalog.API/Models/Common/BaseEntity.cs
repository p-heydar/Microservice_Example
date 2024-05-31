namespace Catalog.API.Models.Common;

public class BaseEntity<Type>
{
    public Type Id { get; set; }
    public DateTime InsertDate { get; set; }
}
