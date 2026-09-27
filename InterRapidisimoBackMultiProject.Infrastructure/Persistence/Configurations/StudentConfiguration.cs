using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterRapidisimoBackMultiProject.Infrastructure.Persistence.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<StudentRecord>
{
    public void Configure(EntityTypeBuilder<StudentRecord> builder)
    {
        builder.ToTable("Students", "dbo");

        builder.HasKey(x => x.Id).HasName("PK_Students");
        builder.Property(x => x.Id).UseIdentityColumn(1, 1);

        builder.Property(x => x.IdentificationNumber).HasColumnType("NVARCHAR(30)").IsRequired();
        builder.Property(x => x.FirstName).HasColumnType("NVARCHAR(100)").IsRequired();
        builder.Property(x => x.LastName).HasColumnType("NVARCHAR(100)").IsRequired();
        builder.Property(x => x.Email).HasColumnType("NVARCHAR(254)").IsRequired();
        builder.Property(x => x.Phone).HasColumnType("NVARCHAR(30)");

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("DATETIME2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("DATETIME2(3)")
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(x => x.IdentificationNumber).IsUnique().HasDatabaseName("UQ_Students_IdentificationNumber");
        builder.HasIndex(x => x.Email).IsUnique().HasDatabaseName("UQ_Students_Email");

        builder.HasOne<AcademicProgramRecord>()
            .WithMany()
            .HasForeignKey(x => x.AcademicProgramId)
            .HasConstraintName("FK_Students_AcademicPrograms")
            .OnDelete(DeleteBehavior.NoAction);
    }
}
