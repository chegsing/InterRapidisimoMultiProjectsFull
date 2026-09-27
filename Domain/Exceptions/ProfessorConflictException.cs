namespace Domain.Exceptions;

/// <summary>
/// Se lanza cuando el estudiante ya tiene una clase con el mismo profesor de la materia a inscribir.
/// </summary>
public sealed class ProfessorConflictException : DomainException
{
    public ProfessorConflictException()
        : base("El estudiante ya tiene una materia con el mismo profesor.")
    {
    }
}
