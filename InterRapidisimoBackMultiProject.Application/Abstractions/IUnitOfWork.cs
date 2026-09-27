namespace InterRapidisimoBackMultiProject.Application.Abstractions;

/// <summary>
/// Puerto de persistencia transaccional. La Application solo sabe que debe "confirmar cambios";
/// desconoce si eso significa SQL Server, PostgreSQL, MongoDB o cualquier otro motor.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
