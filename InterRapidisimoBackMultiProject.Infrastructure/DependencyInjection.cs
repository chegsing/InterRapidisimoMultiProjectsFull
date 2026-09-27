using Domain.Repositories;
using Domain.Services;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence;
using InterRapidisimoBackMultiProject.Infrastructure.Persistence.Repositories;
using InterRapidisimoBackMultiProject.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InterRapidisimoBackMultiProject.Infrastructure;

/// <summary>Punto único de composición de la capa de Infrastructure, invocado desde el host (API).</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer")
            ?? throw new InvalidOperationException("No se configuró la cadena de conexión 'SqlServer'.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IAcademicProgramRepository, AcademicProgramRepository>();
        services.AddScoped<IProfessorRepository, ProfessorRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        services.AddScoped<IClassmateDirectoryService, ClassmateDirectoryService>();

        return services;
    }
}
