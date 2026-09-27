namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

/// <summary>Modelo de persistencia 1:1 con dbo.Professors. No es la entidad de dominio.</summary>
public sealed class ProfessorRecord
{
    public int Id { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<SubjectRecord> Subjects { get; set; } = [];
}
