namespace Domain.Exceptions;

public sealed class InvalidSubjectCodeException : DomainException
{
    public InvalidSubjectCodeException(string message) : base(message)
    {
    }
}
