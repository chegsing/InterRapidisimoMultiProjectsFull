using System.Reflection;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<AcademicProgramRecord> AcademicPrograms => Set<AcademicProgramRecord>();
    public DbSet<ProfessorRecord> Professors => Set<ProfessorRecord>();
    public DbSet<StudentRecord> Students => Set<StudentRecord>();
    public DbSet<SubjectRecord> Subjects => Set<SubjectRecord>();
    public DbSet<StudentSubjectRecord> StudentSubjects => Set<StudentSubjectRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
