using Domain.Entities;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;

/// <summary>Traduce entre el agregado de dominio <see cref="AcademicProgram"/> y su modelo de persistencia.</summary>
public static class AcademicProgramRecordMapper
{
    public static AcademicProgram ToDomain(this AcademicProgramRecord record) =>
        AcademicProgram.Rehydrate(
            new AcademicProgramId(record.Id),
            AcademicProgramName.Create(record.Name),
            AcademicProgramDescription.CreateOptional(record.Description),
            record.CreatedAt,
            record.UpdatedAt,
            record.IsActive);

    public static void ApplyTo(this AcademicProgram academicProgram, AcademicProgramRecord record)
    {
        record.Name = academicProgram.Name.Value;
        record.Description = academicProgram.Description?.Value;
        record.IsActive = academicProgram.IsActive;
        record.CreatedAt = academicProgram.CreatedAtUtc;
        record.UpdatedAt = academicProgram.UpdatedAtUtc;
    }
}
