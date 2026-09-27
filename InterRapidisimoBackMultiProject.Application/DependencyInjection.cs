using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;
using InterRapidisimoBackMultiProject.Application.Features.Classmates.GetClassmates;
using InterRapidisimoBackMultiProject.Application.Features.Enrollment.EnrollStudent;
using InterRapidisimoBackMultiProject.Application.Features.Enrollment.GetStudentSubjects;
using InterRapidisimoBackMultiProject.Application.Features.Enrollment.RemoveSubject;
using InterRapidisimoBackMultiProject.Application.Features.Students.CreateStudent;
using InterRapidisimoBackMultiProject.Application.Features.Students.DeactivateStudent;
using InterRapidisimoBackMultiProject.Application.Features.Students.GetStudent;
using InterRapidisimoBackMultiProject.Application.Features.Students.GetStudents;
using InterRapidisimoBackMultiProject.Application.Features.Students.UpdateStudent;
using InterRapidisimoBackMultiProject.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace InterRapidisimoBackMultiProject.Application;

/// <summary>Punto único de composición de la capa Application: handlers de casos de uso y validators.</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateStudentCommand, int>, CreateStudentHandler>();
        services.AddScoped<IValidator<CreateStudentCommand>, CreateStudentValidator>();

        services.AddScoped<IQueryHandler<GetStudentQuery, StudentDto>, GetStudentHandler>();
        services.AddScoped<IQueryHandler<GetStudentsQuery, IReadOnlyCollection<StudentSummaryDto>>, GetStudentsHandler>();

        services.AddScoped<ICommandHandler<UpdateStudentCommand, Unit>, UpdateStudentHandler>();
        services.AddScoped<IValidator<UpdateStudentCommand>, UpdateStudentValidator>();

        services.AddScoped<ICommandHandler<DeactivateStudentCommand, Unit>, DeactivateStudentHandler>();

        services.AddScoped<ICommandHandler<EnrollStudentCommand, Unit>, EnrollStudentHandler>();
        services.AddScoped<IValidator<EnrollStudentCommand>, EnrollStudentValidator>();

        services.AddScoped<ICommandHandler<RemoveSubjectCommand, Unit>, RemoveSubjectHandler>();

        services.AddScoped<IQueryHandler<GetStudentSubjectsQuery, IReadOnlyCollection<SubjectDto>>, GetStudentSubjectsHandler>();

        services.AddScoped<IQueryHandler<GetClassmatesQuery, IReadOnlyCollection<SubjectClassmatesDto>>, GetClassmatesHandler>();

        return services;
    }
}
