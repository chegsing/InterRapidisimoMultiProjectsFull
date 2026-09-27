using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

/// <summary>
/// Puerto de persistencia para el agregado <see cref="Enrollment"/> (dbo.StudentSubjects), cuya clave es
/// compuesta (StudentId, SubjectId) y por lo tanto no participa de <see cref="IGenericRepository{TEntity, TId}"/>.
/// </summary>
public interface IEnrollmentRepository
{
    Task<IReadOnlyCollection<Enrollment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Enrollment>> GetByStudentIdAsync(StudentId studentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Enrollment>> GetBySubjectIdAsync(SubjectId subjectId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(StudentId studentId, SubjectId subjectId, CancellationToken cancellationToken = default);
    Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken = default);
    Task DeleteAsync(StudentId studentId, SubjectId subjectId, CancellationToken cancellationToken = default);
}
