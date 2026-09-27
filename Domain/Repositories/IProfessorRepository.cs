using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

/// <summary>
/// Puerto de persistencia para el agregado <see cref="Professor"/>.
/// </summary>
public interface IProfessorRepository : IGenericRepository<Professor, ProfessorId>
{
    Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdentificationNumberAsync(IdentificationNumber identificationNumber, CancellationToken cancellationToken = default);
}
