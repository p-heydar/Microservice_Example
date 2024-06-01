namespace Catalog.API.Models.Common;

public class BaseEntity<Type>
    where Type: notnull
{
    public Type Id { get; set; }
    public DateTime InsertDate { get; set; } = DateTime.Now;
}

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime InsertDate { get; set; } = DateTime.Now;
}
