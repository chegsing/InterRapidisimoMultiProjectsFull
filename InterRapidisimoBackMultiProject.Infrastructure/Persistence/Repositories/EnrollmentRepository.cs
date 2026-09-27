using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Repositories;

public sealed class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context) => _context = context;

    public async Task<IReadOnlyCollection<Enrollment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = await _context.StudentSubjects.ToListAsync(cancellationToken);
        return records.Select(r => r.ToDomain()).ToList();
    }

    public async Task<IReadOnlyCollection<Enrollment>> GetByStudentIdAsync(StudentId studentId, CancellationToken cancellationToken = default)
    {
        var records = await _context.StudentSubjects.Where(x => x.StudentId == studentId.Value).ToListAsync(cancellationToken);
        return records.Select(r => r.ToDomain()).ToList();
    }

    public async Task<IReadOnlyCollection<Enrollment>> GetBySubjectIdAsync(SubjectId subjectId, CancellationToken cancellationToken = default)
    {
        var records = await _context.StudentSubjects.Where(x => x.SubjectId == subjectId.Value).ToListAsync(cancellationToken);
        return records.Select(r => r.ToDomain()).ToList();
    }

    public Task<bool> ExistsAsync(StudentId studentId, SubjectId subjectId, CancellationToken cancellationToken = default) =>
        _context.StudentSubjects.AnyAsync(x => x.StudentId == studentId.Value && x.SubjectId == subjectId.Value, cancellationToken);

    public Task AddAsync(Enrollment enrollment, CancellationToken cancellationToken = default)
    {
        _context.StudentSubjects.Add(enrollment.ToRecord());
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(StudentId studentId, SubjectId subjectId, CancellationToken cancellationToken = default)
    {
        var record = await _context.StudentSubjects
            .FirstOrDefaultAsync(x => x.StudentId == studentId.Value && x.SubjectId == subjectId.Value, cancellationToken);

        if (record is not null)
        {
            _context.StudentSubjects.Remove(record);
        }
    }
}
