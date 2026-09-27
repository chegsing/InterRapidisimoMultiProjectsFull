using Domain.Entities;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;

/// <summary>Traduce entre el agregado de dominio <see cref="Professor"/> y su modelo de persistencia.</summary>
public static class ProfessorRecordMapper
{
    public static Professor ToDomain(this ProfessorRecord record, IEnumerable<SubjectId> subjectIds) =>
        Professor.Rehydrate(
            new ProfessorId(record.Id),
            IdentificationNumber.Create(record.IdentificationNumber),
            PersonName.Create(record.FirstName, record.LastName),
            Email.Create(record.Email),
            subjectIds,
            record.CreatedAt,
            record.UpdatedAt,
            record.IsActive);

    public static void ApplyTo(this Professor professor, ProfessorRecord record)
    {
        record.IdentificationNumber = professor.IdentificationNumber.Value;
        record.FirstName = professor.PersonName.FirstName;
        record.LastName = professor.PersonName.LastName;
        record.Email = professor.Email.Value;
        record.IsActive = professor.IsActive;
        record.CreatedAt = professor.CreatedAtUtc;
        record.UpdatedAt = professor.UpdatedAtUtc;
    }
}
