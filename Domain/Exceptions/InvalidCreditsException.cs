namespace Domain.Exceptions;

public sealed class InvalidCreditsException : DomainException
{
    public InvalidCreditsException(string message) : base(message)
    {
    }
}
