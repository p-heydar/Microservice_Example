namespace Catalog.API.Exceptions.Products;

public class EmptyGuidException : Exception
{
    public EmptyGuidException() : base("Your Id Is Guid Empty") { }
}
