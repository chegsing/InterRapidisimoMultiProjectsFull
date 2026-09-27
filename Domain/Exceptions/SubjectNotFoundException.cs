namespace Domain.Exceptions;

public sealed class SubjectNotFoundException : DomainException
{
    public SubjectNotFoundException()
        : base("La materia solicitada no existe.")
    {
    }
}
