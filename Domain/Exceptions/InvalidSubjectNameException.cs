namespace Domain.Exceptions;

public sealed class InvalidSubjectNameException : DomainException
{
    public InvalidSubjectNameException(string message) : base(message)
    {
    }
}
