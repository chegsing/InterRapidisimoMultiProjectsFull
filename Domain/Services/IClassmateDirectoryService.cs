using Domain.ValueObjects;

namespace Domain.Services;

/// <summary>
/// Servicio de dominio que expone, para un estudiante, únicamente los nombres de los
/// compañeros con quienes comparte cada materia (regla de visibilidad restringida).
/// </summary>
public interface IClassmateDirectoryService
{
    IReadOnlyDictionary<SubjectId, IReadOnlyCollection<StudentDirectoryEntry>> GetClassmatesBySubject(
        StudentId requestingStudentId,
        IReadOnlyCollection<Entities.Enrollment> allEnrollments,
        IReadOnlyCollection<Entities.Student> allStudents);
}
