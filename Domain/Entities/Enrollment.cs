using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Representa la inscripción de un estudiante a una materia (dbo.StudentSubjects).
/// Su identidad es la clave compuesta (StudentId, SubjectId), sin columna Id propia.
/// </summary>
public sealed class Enrollment : IEquatable<Enrollment>
{
    public StudentId StudentId { get; }
    public SubjectId SubjectId { get; }
    public DateTime CreatedAtUtc { get; }

    private Enrollment(StudentId studentId, SubjectId subjectId, DateTime createdAtUtc)
    {
        StudentId = studentId;
        SubjectId = subjectId;
        CreatedAtUtc = createdAtUtc;
    }

    internal static Enrollment Create(StudentId studentId, SubjectId subjectId) =>
        new(studentId, subjectId, DateTime.UtcNow);

    public static Enrollment Rehydrate(StudentId studentId, SubjectId subjectId, DateTime createdAtUtc) =>
        new(studentId, subjectId, createdAtUtc);

    public bool Equals(Enrollment? other) =>
        other is not null && StudentId.Equals(other.StudentId) && SubjectId.Equals(other.SubjectId);

    public override bool Equals(object? obj) => Equals(obj as Enrollment);

    public override int GetHashCode() => HashCode.Combine(StudentId, SubjectId);
}
