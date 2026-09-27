using Domain.Common;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>Profesor que dicta hasta <see cref="DomainConstants.MaxSubjectsPerProfessor"/> materias (dbo.Professors).</summary>
public sealed class Professor : AuditableEntity<ProfessorId>
{
    private readonly List<SubjectId> _subjectIds = [];

    public IdentificationNumber IdentificationNumber { get; private set; }
    public PersonName PersonName { get; private set; }
    public Email Email { get; private set; }
    public IReadOnlyCollection<SubjectId> SubjectIds => _subjectIds.AsReadOnly();

    private Professor(
        ProfessorId id,
        IdentificationNumber identificationNumber,
        PersonName personName,
        Email email,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive)
        : base(id, createdAtUtc, updatedAtUtc, isActive)
    {
        IdentificationNumber = identificationNumber;
        PersonName = personName;
        Email = email;
    }

    public static Professor Create(IdentificationNumber identificationNumber, PersonName personName, Email email)
    {
        var now = DateTime.UtcNow;
        return new Professor(ProfessorId.Unassigned, identificationNumber, personName, email, now, now, isActive: true);
    }

    public static Professor Rehydrate(
        ProfessorId id,
        IdentificationNumber identificationNumber,
        PersonName personName,
        Email email,
        IEnumerable<SubjectId> subjectIds,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive)
    {
        var professor = new Professor(id, identificationNumber, personName, email, createdAtUtc, updatedAtUtc, isActive);
        professor._subjectIds.AddRange(subjectIds);
        return professor;
    }

    public void UpdateProfile(PersonName personName, Email email)
    {
        PersonName = personName;
        Email = email;
        MarkAsUpdated();
    }

    /// <summary>
    /// Asigna una materia a este profesor. Regla: máximo <see cref="DomainConstants.MaxSubjectsPerProfessor"/> materias por profesor.
    /// </summary>
    public void AssignSubject(Subject subject)
    {
        if (!subject.ProfessorId.Equals(Id))
        {
            throw new SubjectProfessorMismatchException();
        }

        if (_subjectIds.Contains(subject.Id))
        {
            return;
        }

        if (_subjectIds.Count >= DomainConstants.MaxSubjectsPerProfessor)
        {
            throw new MaxSubjectsPerProfessorExceededException();
        }

        _subjectIds.Add(subject.Id);
        MarkAsUpdated();
    }
}
