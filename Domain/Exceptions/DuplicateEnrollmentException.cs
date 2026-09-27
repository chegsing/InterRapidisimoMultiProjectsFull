namespace Domain.Exceptions;

public sealed class DuplicateEnrollmentException : DomainException
{
    public DuplicateEnrollmentException()
        : base("El estudiante ya se encuentra inscrito en esta materia.")
    {
    }
}
