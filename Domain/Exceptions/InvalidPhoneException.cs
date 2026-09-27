namespace Domain.Exceptions;

public sealed class InvalidPhoneException : DomainException
{
    public InvalidPhoneException(string message) : base(message)
    {
    }
}
