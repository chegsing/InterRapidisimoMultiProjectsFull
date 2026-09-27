using Domain.Entities;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;

/// <summary>Traduce entre el agregado de dominio <see cref="Student"/> y su modelo de persistencia.</summary>
public static class StudentRecordMapper
{
    public static Student ToDomain(this StudentRecord record, IEnumerable<Enrollment> enrollments) =>
        Student.Rehydrate(
            new StudentId(record.Id),
            IdentificationNumber.Create(record.IdentificationNumber),
            PersonName.Create(record.FirstName, record.LastName),
            Email.Create(record.Email),
            Phone.CreateOptional(record.Phone),
            new AcademicProgramId(record.AcademicProgramId),
            enrollments,
            record.CreatedAt,
            record.UpdatedAt,
            record.IsActive);

    public static void ApplyTo(this Student student, StudentRecord record)
    {
        record.IdentificationNumber = student.IdentificationNumber.Value;
        record.FirstName = student.PersonName.FirstName;
        record.LastName = student.PersonName.LastName;
        record.Email = student.Email.Value;
        record.Phone = student.Phone?.Value;
        record.AcademicProgramId = student.AcademicProgramId.Value;
        record.IsActive = student.IsActive;
        record.CreatedAt = student.CreatedAtUtc;
        record.UpdatedAt = student.UpdatedAtUtc;
    }
}
