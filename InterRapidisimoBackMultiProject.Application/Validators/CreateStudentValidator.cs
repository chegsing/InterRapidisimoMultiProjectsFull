using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Features.Students.CreateStudent;

namespace InterRapidisimoBackMultiProject.Application.Validators;

/// <summary>
/// Valida las reglas de negocio de <see cref="CreateStudentCommand"/> que requieren consultar puertos
/// (unicidad de correo/identificación, existencia del programa académico).
/// </summary>
public sealed class CreateStudentValidator : IValidator<CreateStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IAcademicProgramRepository _academicProgramRepository;

    public CreateStudentValidator(IStudentRepository studentRepository, IAcademicProgramRepository academicProgramRepository)
    {
        _studentRepository = studentRepository;
        _academicProgramRepository = academicProgramRepository;
    }

    public async Task<IReadOnlyCollection<string>> ValidateAsync(CreateStudentCommand request, CancellationToken cancellationToken = default)
    {
        var errors = new List<string>();

        var academicProgram = await _academicProgramRepository.GetByIdAsync(new AcademicProgramId(request.AcademicProgramId), cancellationToken);
        if (academicProgram is null)
        {
            errors.Add($"El programa académico '{request.AcademicProgramId}' no existe.");
        }

        if (!string.IsNullOrWhiteSpace(request.Email) && await _studentRepository.ExistsWithEmailAsync(Email.Create(request.Email), cancellationToken))
        {
            errors.Add($"Ya existe un estudiante registrado con el correo '{request.Email}'.");
        }

        if (!string.IsNullOrWhiteSpace(request.IdentificationNumber) &&
            await _studentRepository.ExistsWithIdentificationNumberAsync(IdentificationNumber.Create(request.IdentificationNumber), cancellationToken))
        {
            errors.Add($"Ya existe un estudiante registrado con la identificación '{request.IdentificationNumber}'.");
        }

        return errors;
    }
}
