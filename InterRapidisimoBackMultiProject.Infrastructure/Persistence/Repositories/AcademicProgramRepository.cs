using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Repositories;

public sealed class AcademicProgramRepository : IAcademicProgramRepository
{
    private readonly ApplicationDbContext _context;

    public AcademicProgramRepository(ApplicationDbContext context) => _context = context;

    public async Task<AcademicProgram?> GetByIdAsync(AcademicProgramId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.AcademicPrograms.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return record?.ToDomain();
    }

    public async Task<IReadOnlyCollection<AcademicProgram>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = await _context.AcademicPrograms.ToListAsync(cancellationToken);
        return records.Select(r => r.ToDomain()).ToList();
    }

    public Task<bool> ExistsWithNameAsync(AcademicProgramName name, CancellationToken cancellationToken = default) =>
        _context.AcademicPrograms.AnyAsync(x => x.Name == name.Value, cancellationToken);

    public async Task AddAsync(AcademicProgram academicProgram, CancellationToken cancellationToken = default)
    {
        var record = new AcademicProgramRecord();
        academicProgram.ApplyTo(record);

        _context.AcademicPrograms.Add(record);

        // Se confirma de inmediato para obtener el Id IDENTITY generado por SQL Server y reflejarlo en el agregado.
        await _context.SaveChangesAsync(cancellationToken);
        academicProgram.AssignGeneratedId(new AcademicProgramId(record.Id));
    }

    public async Task UpdateAsync(AcademicProgram academicProgram, CancellationToken cancellationToken = default)
    {
        var record = await _context.AcademicPrograms.FirstOrDefaultAsync(x => x.Id == academicProgram.Id.Value, cancellationToken)
            ?? throw new InvalidOperationException($"No se encontró el registro de AcademicPrograms con Id '{academicProgram.Id.Value}' para actualizar.");

        academicProgram.ApplyTo(record);
    }

    public async Task DeleteAsync(AcademicProgramId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.AcademicPrograms.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        if (record is not null)
        {
            _context.AcademicPrograms.Remove(record);
        }
    }
}
