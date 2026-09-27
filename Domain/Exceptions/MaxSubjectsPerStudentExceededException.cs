using Domain.Common;

namespace Domain.Exceptions;

/// <summary>
/// Se lanza cuando un estudiante intenta inscribirse en más de <see cref="DomainConstants.MaxSubjectsPerStudent"/> materias.
/// </summary>
public sealed class MaxSubjectsPerStudentExceededException : DomainException
{
    public MaxSubjectsPerStudentExceededException()
        : base($"Un estudiante no puede inscribirse en más de {DomainConstants.MaxSubjectsPerStudent} materias.")
    {
    }
}
