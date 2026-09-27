namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;

/// <summary>Modelo de persistencia 1:1 con dbo.AcademicPrograms. No es la entidad de dominio.</summary>
public sealed class AcademicProgramRecord
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
