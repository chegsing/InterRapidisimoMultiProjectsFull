using InterRapidisimoBackMultiProject.Application.Abstractions;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.DeactivateStudent;

public sealed record DeactivateStudentCommand(int StudentId) : ICommand<Unit>;
