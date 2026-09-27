using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Repositories;

public sealed class ProfessorRepository : IProfessorRepository
{
    private readonly ApplicationDbContext _context;

    public ProfessorRepository(ApplicationDbContext context) => _context = context;

    public async Task<Professor?> GetByIdAsync(ProfessorId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.Professors.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        if (record is null)
        {
            return null;
        }

        var subjectIds = await _context.Subjects
            .Where(x => x.ProfessorId == id.Value)
            .Select(x => new SubjectId(x.Id))
            .ToListAsync(cancellationToken);

        return record.ToDomain(subjectIds);
    }

    public async Task<IReadOnlyCollection<Professor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = await _context.Professors.ToListAsync(cancellationToken);
        var subjects = await _context.Subjects.ToListAsync(cancellationToken);

        return records
            .Select(record => record.ToDomain(subjects
                .Where(s => s.ProfessorId == record.Id)
                .Select(s => new SubjectId(s.Id))))
            .ToList();
    }

    public Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default) =>
        _context.Professors.AnyAsync(x => x.Email == email.Value, cancellationToken);

    public Task<bool> ExistsWithIdentificationNumberAsync(IdentificationNumber identificationNumber, CancellationToken cancellationToken = default) =>
        _context.Professors.AnyAsync(x => x.IdentificationNumber == identificationNumber.Value, cancellationToken);

    public async Task AddAsync(Professor professor, CancellationToken cancellationToken = default)
    {
        var record = new ProfessorRecord();
        professor.ApplyTo(record);

        _context.Professors.Add(record);

        // Se confirma de inmediato para obtener el Id IDENTITY generado por SQL Server y reflejarlo en el agregado.
        await _context.SaveChangesAsync(cancellationToken);
        professor.AssignGeneratedId(new ProfessorId(record.Id));
    }

    public async Task UpdateAsync(Professor professor, CancellationToken cancellationToken = default)
    {
        var record = await _context.Professors.FirstOrDefaultAsync(x => x.Id == professor.Id.Value, cancellationToken)
            ?? throw new InvalidOperationException($"No se encontró el registro de Professors con Id '{professor.Id.Value}' para actualizar.");

        professor.ApplyTo(record);
    }

    public async Task DeleteAsync(ProfessorId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.Professors.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        if (record is not null)
        {
            _context.Professors.Remove(record);
        }
    }
}
