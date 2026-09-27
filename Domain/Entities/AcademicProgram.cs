using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>Programa académico de créditos al cual se adhiere un estudiante (dbo.AcademicPrograms).</summary>
public sealed class AcademicProgram : AuditableEntity<AcademicProgramId>
{
    public AcademicProgramName Name { get; private set; }
    public AcademicProgramDescription? Description { get; private set; }

    private AcademicProgram(
        AcademicProgramId id,
        AcademicProgramName name,
        AcademicProgramDescription? description,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive)
        : base(id, createdAtUtc, updatedAtUtc, isActive)
    {
        Name = name;
        Description = description;
    }

    public static AcademicProgram Create(AcademicProgramName name, AcademicProgramDescription? description)
    {
        var now = DateTime.UtcNow;
        return new AcademicProgram(AcademicProgramId.Unassigned, name, description, now, now, isActive: true);
    }

    public static AcademicProgram Rehydrate(
        AcademicProgramId id,
        AcademicProgramName name,
        AcademicProgramDescription? description,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive) =>
        new(id, name, description, createdAtUtc, updatedAtUtc, isActive);

    public void UpdateDetails(AcademicProgramName name, AcademicProgramDescription? description)
    {
        Name = name;
        Description = description;
        MarkAsUpdated();
    }
}
