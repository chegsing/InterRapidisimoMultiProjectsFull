using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Repositories;

public sealed class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context) => _context = context;

    public async Task<Student?> GetByIdAsync(StudentId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.Students.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        if (record is null)
        {
            return null;
        }

        var enrollments = await _context.StudentSubjects
            .Where(x => x.StudentId == id.Value)
            .Select(x => x.ToDomain())
            .ToListAsync(cancellationToken);

        return record.ToDomain(enrollments);
    }

    public async Task<IReadOnlyCollection<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = await _context.Students.ToListAsync(cancellationToken);
        var enrollmentsByStudent = await _context.StudentSubjects.ToListAsync(cancellationToken);

        return records
            .Select(record => record.ToDomain(enrollmentsByStudent
                .Where(e => e.StudentId == record.Id)
                .Select(e => e.ToDomain())))
            .ToList();
    }

    public Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default) =>
        _context.Students.AnyAsync(x => x.Email == email.Value, cancellationToken);

    public Task<bool> ExistsWithIdentificationNumberAsync(IdentificationNumber identificationNumber, CancellationToken cancellationToken = default) =>
        _context.Students.AnyAsync(x => x.IdentificationNumber == identificationNumber.Value, cancellationToken);

    public async Task AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        var record = new StudentRecord();
        student.ApplyTo(record);

        _context.Students.Add(record);

        // Se confirma de inmediato para obtener el Id IDENTITY generado por SQL Server y reflejarlo en el agregado.
        await _context.SaveChangesAsync(cancellationToken);
        student.AssignGeneratedId(new StudentId(record.Id));
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        var record = await _context.Students.FirstOrDefaultAsync(x => x.Id == student.Id.Value, cancellationToken)
            ?? throw new InvalidOperationException($"No se encontró el registro de Students con Id '{student.Id.Value}' para actualizar.");

        student.ApplyTo(record);
    }

    public async Task DeleteAsync(StudentId id, CancellationToken cancellationToken = default)
    {
        var record = await _context.Students.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        if (record is not null)
        {
            _context.Students.Remove(record);
        }
    }
}
