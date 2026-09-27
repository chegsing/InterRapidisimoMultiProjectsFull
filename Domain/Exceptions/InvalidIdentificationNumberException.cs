namespace Domain.Exceptions;

public sealed class InvalidIdentificationNumberException : DomainException
{
    public InvalidIdentificationNumberException(string message) : base(message)
    {
    }
}
