using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence;

namespace InterRapidisimoBackMultiProject.Infrastructure.Services;

/// <summary>Adaptador de <see cref="IUnitOfWork"/> sobre EF Core: confirma todos los cambios pendientes del DbContext.</summary>
public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EfUnitOfWork(ApplicationDbContext context) => _context = context;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
