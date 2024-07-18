namespace BuildingBlocks.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string entityName, object key) : base($"{entityName} By Id {key} Was Not Found")
    {
    }
}