using Domain.Entities;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;

/// <summary>Traduce entre el agregado de dominio <see cref="Enrollment"/> y su modelo de persistencia (dbo.StudentSubjects).</summary>
public static class EnrollmentRecordMapper
{
    public static Enrollment ToDomain(this StudentSubjectRecord record) =>
        Enrollment.Rehydrate(new StudentId(record.StudentId), new SubjectId(record.SubjectId), record.CreatedAt);

    public static StudentSubjectRecord ToRecord(this Enrollment enrollment) => new()
    {
        StudentId = enrollment.StudentId.Value,
        SubjectId = enrollment.SubjectId.Value,
        CreatedAt = enrollment.CreatedAtUtc,
    };
}
