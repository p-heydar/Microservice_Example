namespace Catalog.API.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string entityName, Guid id) : base($"{entityName} By Id {id} Was Not Found")
    {
    }
}