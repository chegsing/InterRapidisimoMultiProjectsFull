namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

/// <summary>Modelo de persistencia 1:1 con dbo.StudentSubjects (tabla puente, clave compuesta).</summary>
public sealed class StudentSubjectRecord
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public DateTime CreatedAt { get; set; }

    public StudentRecord? Student { get; set; }
    public SubjectRecord? Subject { get; set; }
}
