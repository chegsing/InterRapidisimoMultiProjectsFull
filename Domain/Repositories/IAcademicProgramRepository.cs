using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

/// <summary>
/// Puerto de persistencia para el agregado <see cref="AcademicProgram"/>.
/// </summary>
public interface IAcademicProgramRepository : IGenericRepository<AcademicProgram, AcademicProgramId>
{
    Task<bool> ExistsWithNameAsync(AcademicProgramName name, CancellationToken cancellationToken = default);
}
