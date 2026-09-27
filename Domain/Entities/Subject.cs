using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Materia académica (dbo.Subjects). Toda materia otorga <see cref="DomainConstants.CreditsPerSubject"/> créditos
/// y es dictada por un único profesor.
/// </summary>
public sealed class Subject : AuditableEntity<SubjectId>
{
    public SubjectCode Code { get; private set; }
    public SubjectName Name { get; private set; }
    public Credits Credits { get; }
    public ProfessorId ProfessorId { get; }

    private Subject(
        SubjectId id,
        SubjectCode code,
        SubjectName name,
        ProfessorId professorId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive)
        : base(id, createdAtUtc, updatedAtUtc, isActive)
    {
        Code = code;
        Name = name;
        Credits = Credits.Default;
        ProfessorId = professorId;
    }

    public static Subject Create(SubjectCode code, SubjectName name, ProfessorId professorId)
    {
        var now = DateTime.UtcNow;
        return new Subject(SubjectId.Unassigned, code, name, professorId, now, now, isActive: true);
    }

    public static Subject Rehydrate(
        SubjectId id,
        SubjectCode code,
        SubjectName name,
        ProfessorId professorId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive) =>
        new(id, code, name, professorId, createdAtUtc, updatedAtUtc, isActive);

    public void Rename(SubjectName name)
    {
        Name = name;
        MarkAsUpdated();
    }
}
