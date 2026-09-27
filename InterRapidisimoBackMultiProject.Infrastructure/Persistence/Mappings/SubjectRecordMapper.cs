using Domain.Entities;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;

/// <summary>Traduce entre el agregado de dominio <see cref="Subject"/> y su modelo de persistencia.</summary>
public static class SubjectRecordMapper
{
    public static Subject ToDomain(this SubjectRecord record) =>
        Subject.Rehydrate(
            new SubjectId(record.Id),
            SubjectCode.Create(record.Code),
            SubjectName.Create(record.Name),
            new ProfessorId(record.ProfessorId),
            record.CreatedAt,
            record.UpdatedAt,
            record.IsActive);

    public static void ApplyTo(this Subject subject, SubjectRecord record)
    {
        record.Code = subject.Code.Value;
        record.Name = subject.Name.Value;
        record.Credits = (byte)subject.Credits.Value;
        record.ProfessorId = subject.ProfessorId.Value;
        record.IsActive = subject.IsActive;
        record.CreatedAt = subject.CreatedAtUtc;
        record.UpdatedAt = subject.UpdatedAtUtc;
    }
}
