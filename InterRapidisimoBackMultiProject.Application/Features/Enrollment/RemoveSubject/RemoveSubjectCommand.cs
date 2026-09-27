using InterRapidisimoBackMultiProject.Application.Abstractions;

namespace InterRapidisimoBackMultiProject.Application.Features.Enrollment.RemoveSubject;

/// <summary>Caso de uso: el estudiante retira una materia inscrita.</summary>
public sealed record RemoveSubjectCommand(int StudentId, int SubjectId) : ICommand<Unit>;
