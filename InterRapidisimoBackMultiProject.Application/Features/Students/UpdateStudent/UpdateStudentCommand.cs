using InterRapidisimoBackMultiProject.Application.Abstractions;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.UpdateStudent;

public sealed record UpdateStudentCommand(
    int StudentId,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    int AcademicProgramId) : ICommand<Unit>;
