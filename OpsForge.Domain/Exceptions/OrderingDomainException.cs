
namespace OpsForge.Domain.Exceptions;

public class OrderingDomainException : DomainException
{
    public OrderingDomainException(string message, object exception) 
        : base(message, exception) { }
}
