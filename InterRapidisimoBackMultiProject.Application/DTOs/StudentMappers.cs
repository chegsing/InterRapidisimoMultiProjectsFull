using Domain.Entities;
using Domain.ValueObjects;

namespace InterRapidisimoBackMultiProject.Application.DTOs;

/// <summary>Traduce entidades del dominio <see cref="Student"/> a los DTOs que expone la Application.</summary>
public static class StudentMappers
{
    public static StudentDto ToDto(this Student student) => new(
        student.Id.Value,
        student.IdentificationNumber.Value,
        student.PersonName.FirstName,
        student.PersonName.LastName,
        student.Email.Value,
        student.Phone?.Value,
        student.AcademicProgramId.Value,
        student.IsActive,
        student.CreatedAtUtc,
        student.UpdatedAtUtc);

    public static StudentSummaryDto ToSummaryDto(this Student student) => new(
        student.Id.Value,
        student.PersonName.FullName,
        student.AcademicProgramId.Value);

    public static ClassmateDto ToClassmateDto(this StudentDirectoryEntry entry) => new(
        entry.StudentId.Value,
        entry.PersonName.FullName);
}
