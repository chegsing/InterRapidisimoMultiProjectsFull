using InterRapidisimoBackMultiProject.Application.Abstractions;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.CreateStudent;

/// <summary>Caso de uso: registro en línea de un nuevo estudiante.</summary>
public sealed record CreateStudentCommand(
    string IdentificationNumber,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    int AcademicProgramId) : ICommand<int>;
