using Domain.Common;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Estudiante que se adhiere al programa de créditos (dbo.Students). Reglas aplicadas al inscribirse a una materia:
/// máximo <see cref="DomainConstants.MaxSubjectsPerStudent"/> materias, sin duplicados y sin repetir profesor.
/// </summary>
public sealed class Student : AuditableEntity<StudentId>
{
    private readonly List<Enrollment> _enrollments = [];

    public IdentificationNumber IdentificationNumber { get; private set; }
    public PersonName PersonName { get; private set; }
    public Email Email { get; private set; }
    public Phone? Phone { get; private set; }
    public AcademicProgramId AcademicProgramId { get; private set; }
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

    private Student(
        StudentId id,
        IdentificationNumber identificationNumber,
        PersonName personName,
        Email email,
        Phone? phone,
        AcademicProgramId academicProgramId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive)
        : base(id, createdAtUtc, updatedAtUtc, isActive)
    {
        IdentificationNumber = identificationNumber;
        PersonName = personName;
        Email = email;
        Phone = phone;
        AcademicProgramId = academicProgramId;
    }

    public static Student Create(
        IdentificationNumber identificationNumber,
        PersonName personName,
        Email email,
        Phone? phone,
        AcademicProgramId academicProgramId)
    {
        var now = DateTime.UtcNow;
        return new Student(StudentId.Unassigned, identificationNumber, personName, email, phone, academicProgramId, now, now, isActive: true);
    }

    public static Student Rehydrate(
        StudentId id,
        IdentificationNumber identificationNumber,
        PersonName personName,
        Email email,
        Phone? phone,
        AcademicProgramId academicProgramId,
        IEnumerable<Enrollment> enrollments,
        DateTime createdAtUtc,
        DateTime updatedAtUtc,
        bool isActive)
    {
        var student = new Student(id, identificationNumber, personName, email, phone, academicProgramId, createdAtUtc, updatedAtUtc, isActive);
        student._enrollments.AddRange(enrollments);
        return student;
    }

    public void UpdateProfile(PersonName personName, Email email, Phone? phone, AcademicProgramId academicProgramId)
    {
        PersonName = personName;
        Email = email;
        Phone = phone;
        AcademicProgramId = academicProgramId;
        MarkAsUpdated();
    }

    /// <summary>
    /// Inscribe al estudiante en una materia validando las reglas del programa de créditos.
    /// </summary>
    /// <param name="subject">Materia a inscribir.</param>
    /// <param name="currentlyEnrolledSubjects">
    /// Materias ya inscritas por el estudiante (deben corresponder a <see cref="Enrollments"/>), usadas para
    /// verificar que ninguna comparta profesor con <paramref name="subject"/>.
    /// </param>
    public Enrollment EnrollIn(Subject subject, IReadOnlyCollection<Subject> currentlyEnrolledSubjects)
    {
        if (_enrollments.Any(e => e.SubjectId.Equals(subject.Id)))
        {
            throw new DuplicateEnrollmentException();
        }

        if (_enrollments.Count >= DomainConstants.MaxSubjectsPerStudent)
        {
            throw new MaxSubjectsPerStudentExceededException();
        }

        if (currentlyEnrolledSubjects.Any(s => s.ProfessorId.Equals(subject.ProfessorId)))
        {
            throw new ProfessorConflictException();
        }

        var enrollment = Enrollment.Create(Id, subject.Id);
        _enrollments.Add(enrollment);
        MarkAsUpdated();
        return enrollment;
    }

    public void Withdraw(SubjectId subjectId)
    {
        _enrollments.RemoveAll(e => e.SubjectId.Equals(subjectId));
        MarkAsUpdated();
    }

    /// <summary>
    /// Proyección segura para que otros estudiantes puedan ver su registro sin exponer datos sensibles.
    /// </summary>
    public StudentDirectoryEntry ToDirectoryEntry() => new(Id, PersonName);
}
