namespace Ordering.Domain.Exceptions;

public class IdCannotEmptyException:DomainException
{
    public IdCannotEmptyException(string idType) : base($"{idType} Cannot Be Empty") {}
}