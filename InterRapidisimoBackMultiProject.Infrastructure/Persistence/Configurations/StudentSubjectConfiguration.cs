using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Configurations;

public sealed class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubjectRecord>
{
    public void Configure(EntityTypeBuilder<StudentSubjectRecord> builder)
    {
        builder.ToTable("StudentSubjects", "dbo");

        builder.HasKey(x => new { x.StudentId, x.SubjectId }).HasName("PK_StudentSubjects");

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(x => x.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(x => x.StudentId)
            .HasConstraintName("FK_StudentSubjects_Students")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Subject)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(x => x.SubjectId)
            .HasConstraintName("FK_StudentSubjects_Subjects")
            .OnDelete(DeleteBehavior.NoAction);
    }
}
