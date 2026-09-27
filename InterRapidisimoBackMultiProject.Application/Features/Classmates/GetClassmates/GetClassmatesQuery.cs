using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;

namespace InterRapidisimoBackMultiProject.Application.Features.Classmates.GetClassmates;

/// <summary>Regla: el estudiante solo puede ver el nombre de los alumnos con quienes comparte cada clase.</summary>
public sealed record GetClassmatesQuery(int StudentId) : IQuery<IReadOnlyCollection<SubjectClassmatesDto>>;
