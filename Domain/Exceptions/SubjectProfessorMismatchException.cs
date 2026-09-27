namespace Domain.Exceptions;

/// <summary>
/// Se lanza cuando se intenta asignar a un profesor una materia que pertenece a otro profesor.
/// </summary>
public sealed class SubjectProfessorMismatchException : DomainException
{
    public SubjectProfessorMismatchException()
        : base("La materia no está asociada a este profesor.")
    {
    }
}
