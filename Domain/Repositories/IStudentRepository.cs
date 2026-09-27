using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

/// <summary>
/// Puerto de persistencia para el agregado <see cref="Student"/>.
/// </summary>
public interface IStudentRepository : IGenericRepository<Student, StudentId>
{
    Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdentificationNumberAsync(IdentificationNumber identificationNumber, CancellationToken cancellationToken = default);
}
