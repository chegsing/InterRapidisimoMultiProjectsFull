using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

/// <summary>
/// Puerto de persistencia para el agregado <see cref="Subject"/>.
/// </summary>
public interface ISubjectRepository : IGenericRepository<Subject, SubjectId>
{
    Task<bool> ExistsWithCodeAsync(SubjectCode code, CancellationToken cancellationToken = default);
}
