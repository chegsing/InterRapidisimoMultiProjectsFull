namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

/// <summary>Modelo de persistencia 1:1 con dbo.Students. No es la entidad de dominio.</summary>
public sealed class StudentRecord
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public int AcademicProgramId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<StudentSubjectRecord> StudentSubjects { get; set; } = [];
}
