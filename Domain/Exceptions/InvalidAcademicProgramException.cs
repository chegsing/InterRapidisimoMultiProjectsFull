namespace Domain.Exceptions;

public sealed class InvalidAcademicProgramException : DomainException
{
    public InvalidAcademicProgramException(string message) : base(message)
    {
    }
}
