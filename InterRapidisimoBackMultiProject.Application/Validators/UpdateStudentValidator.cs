using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Exceptions;
using InterRapidisimoBackMultiProject.Application.Features.Students.UpdateStudent;

namespace InterRapidisimoBackMultiProject.Application.Validators;

/// <summary>Valida que el nuevo correo y el programa académico sean consistentes antes de actualizar el perfil.</summary>
public sealed class UpdateStudentValidator : IValidator<UpdateStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IAcademicProgramRepository _academicProgramRepository;

    public UpdateStudentValidator(IStudentRepository studentRepository, IAcademicProgramRepository academicProgramRepository)
    {
        _studentRepository = studentRepository;
        _academicProgramRepository = academicProgramRepository;
    }

    public async Task<IReadOnlyCollection<string>> ValidateAsync(UpdateStudentCommand request, CancellationToken cancellationToken = default)
    {
        var errors = new List<string>();

        if (await _academicProgramRepository.GetByIdAsync(new AcademicProgramId(request.AcademicProgramId), cancellationToken) is null)
        {
            errors.Add($"El programa académico '{request.AcademicProgramId}' no existe.");
        }

        var existingStudent = await _studentRepository.GetByIdAsync(new StudentId(request.StudentId), cancellationToken);
        if (existingStudent is not null &&
            !string.Equals(existingStudent.Email.Value, request.Email, StringComparison.OrdinalIgnoreCase) &&
            await _studentRepository.ExistsWithEmailAsync(Email.Create(request.Email), cancellationToken))
        {
            errors.Add($"Ya existe un estudiante registrado con el correo '{request.Email}'.");
        }

        return errors;
    }
}
