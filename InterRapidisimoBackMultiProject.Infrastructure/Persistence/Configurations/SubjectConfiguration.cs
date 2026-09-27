using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Configurations;

public sealed class SubjectConfiguration : IEntityTypeConfiguration<SubjectRecord>
{
    public void Configure(EntityTypeBuilder<SubjectRecord> builder)
    {
        builder.ToTable("Subjects", "dbo", t => t.HasCheckConstraint("CK_Subjects_Credits", "[Credits] = 3"));

        builder.HasKey(x => x.Id).HasName("PK_Subjects");
        builder.Property(x => x.Id).UseIdentityColumn(1, 1);

        builder.Property(x => x.Code).HasColumnType("NVARCHAR(30)").IsRequired();
        builder.Property(x => x.Name).HasColumnType("NVARCHAR(150)").IsRequired();
        builder.Property(x => x.Credits).HasColumnType("TINYINT").IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(x => x.Code).IsUnique().HasDatabaseName("UQ_Subjects_Code");

        builder.HasOne(x => x.Professor)
            .WithMany(p => p.Subjects)
            .HasForeignKey(x => x.ProfessorId)
            .HasConstraintName("FK_Subjects_Professors")
            .OnDelete(DeleteBehavior.NoAction);
    }
}
