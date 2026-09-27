using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Repositories;

public sealed class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _context;

    public SubjectRepository(ApplicationDbContext context) => _context = context;

    public async Task<Subject?> GetByIdAsync(SubjectId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return record?.ToDomain();
    }

    public async Task<IReadOnlyCollection<Subject>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = await _context.Subjects.ToListAsync(cancellationToken);
        return records.Select(r => r.ToDomain()).ToList();
    }

    public Task<bool> ExistsWithCodeAsync(SubjectCode code, CancellationToken cancellationToken = default) =>
        _context.Subjects.AnyAsync(x => x.Code == code.Value, cancellationToken);

    public async Task AddAsync(Subject subject, CancellationToken cancellationToken = default)
    {
        var record = new SubjectRecord();
        subject.ApplyTo(record);

        _context.Subjects.Add(record);

        // Se confirma de inmediato para obtener el Id IDENTITY generado por SQL Server y reflejarlo en el agregado.
        await _context.SaveChangesAsync(cancellationToken);
        subject.AssignGeneratedId(new SubjectId(record.Id));
    }

    public async Task UpdateAsync(Subject subject, CancellationToken cancellationToken = default)
    {
        var record = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == subject.Id.Value, cancellationToken)
            ?? throw new InvalidOperationException($"No se encontró el registro de Subjects con Id '{subject.Id.Value}' para actualizar.");

        subject.ApplyTo(record);
    }

    public async Task DeleteAsync(SubjectId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        if (record is not null)
        {
            _context.Subjects.Remove(record);
        }
    }
}
