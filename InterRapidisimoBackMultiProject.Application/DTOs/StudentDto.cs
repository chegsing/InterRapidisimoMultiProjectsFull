namespace InterRapidisimoBackMultiProject.Application.DTOs;

/// <summary>Vista completa de un estudiante, para su propio dueño o para administración.</summary>
public sealed record StudentDto(
    int Id,
    string IdentificationNumber,
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    int AcademicProgramId,
    bool IsActive,
    DateTime CreatedAtUtc,
    DateTime UpdatedAtUtc);
