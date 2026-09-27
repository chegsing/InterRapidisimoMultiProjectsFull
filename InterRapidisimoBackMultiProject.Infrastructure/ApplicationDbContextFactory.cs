using InterRapidisimoBackMultiProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InterRapidisimoBackMultiProject.Infrastructure;

/// <summary>
/// Permite a las herramientas de diseño de EF Core (dotnet ef migrations) crear el DbContext
/// sin depender de un proyecto de host ejecutable.
/// </summary>
public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=StudentRegistrationDB;User Id=desarrollo;Password=1nt3rr4p1d1s1m0;TrustServerCertificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
