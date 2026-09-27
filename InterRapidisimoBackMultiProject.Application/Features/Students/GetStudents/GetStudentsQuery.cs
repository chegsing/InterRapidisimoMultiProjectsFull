using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.GetStudents;

/// <summary>Regla: cualquier estudiante puede ver en línea los registros de otros estudiantes (vista pública).</summary>
public sealed record GetStudentsQuery : IQuery<IReadOnlyCollection<StudentSummaryDto>>;
