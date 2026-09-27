using Domain.Repositories;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Features.Enrollment.EnrollStudent;

namespace InterRapidisimoBackMultiProject.Application.Validators;

/// <summary>Valida que el estudiante y la materia existan antes de intentar la inscripción.</summary>
public sealed class EnrollStudentValidator : IValidator<EnrollStudentCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;

    public EnrollStudentValidator(IStudentRepository studentRepository, ISubjectRepository subjectRepository)
    {
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<IReadOnlyCollection<string>> ValidateAsync(EnrollStudentCommand request, CancellationToken cancellationToken = default)
    {
        var errors = new List<string>();

        if (await _studentRepository.GetByIdAsync(new Domain.ValueObjects.StudentId(request.StudentId), cancellationToken) is null)
        {
            errors.Add($"El estudiante '{request.StudentId}' no existe.");
        }

        if (await _subjectRepository.GetByIdAsync(new Domain.ValueObjects.SubjectId(request.SubjectId), cancellationToken) is null)
        {
            errors.Add($"La materia '{request.SubjectId}' no existe.");
        }

        return errors;
    }
}
