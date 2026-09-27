using Domain.Common;

namespace Domain.Exceptions;

/// <summary>
/// Se lanza cuando un profesor ya dicta <see cref="DomainConstants.MaxSubjectsPerProfessor"/> materias y se intenta asignarle otra.
/// </summary>
public sealed class MaxSubjectsPerProfessorExceededException : DomainException
{
    public MaxSubjectsPerProfessorExceededException()
        : base($"Un profesor no puede dictar más de {DomainConstants.MaxSubjectsPerProfessor} materias.")
    {
    }
}
