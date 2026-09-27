using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Configurations;

public sealed class AcademicProgramConfiguration : IEntityTypeConfiguration<AcademicProgramRecord>
{
    public void Configure(EntityTypeBuilder<AcademicProgramRecord> builder)
    {
        builder.ToTable("AcademicPrograms", "dbo");

        builder.HasKey(x => x.Id).HasName("PK_AcademicPrograms");
        builder.Property(x => x.Id).UseIdentityColumn(1, 1);

        builder.Property(x => x.Name).HasColumnType("NVARCHAR(150)").IsRequired();
        builder.Property(x => x.Description).HasColumnType("NVARCHAR(500)");

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("UQ_AcademicPrograms_Name");
    }
}
