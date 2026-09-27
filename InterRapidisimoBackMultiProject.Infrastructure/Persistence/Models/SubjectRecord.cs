namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

/// <summary>Modelo de persistencia 1:1 con dbo.Subjects. No es la entidad de dominio.</summary>
public sealed class SubjectRecord
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public byte Credits { get; set; }
    public int ProfessorId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ProfessorRecord? Professor { get; set; }
    public List<StudentSubjectRecord> StudentSubjects { get; set; } = [];
}
