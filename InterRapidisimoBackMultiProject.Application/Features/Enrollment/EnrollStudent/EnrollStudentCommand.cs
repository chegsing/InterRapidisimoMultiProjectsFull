using InterRapidisimoBackMultiProject.Application.Abstractions;

namespace InterRapidisimoBackMultiProject.Application.Features.Enrollment.EnrollStudent;

/// <summary>Caso de uso: el estudiante se adhiere a una materia del programa de créditos.</summary>
public sealed record EnrollStudentCommand(int StudentId, int SubjectId) : ICommand<Unit>;
