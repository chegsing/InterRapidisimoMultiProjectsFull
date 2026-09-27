using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Enrollment.GetStudentSubjects;

public sealed class GetStudentSubjectsHandler : IQueryHandler<GetStudentSubjectsQuery, IReadOnlyCollection<SubjectDto>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IProfessorRepository _professorRepository;

    public GetStudentSubjectsHandler(IStudentRepository studentRepository, ISubjectRepository subjectRepository, IProfessorRepository professorRepository)
    {
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
        _professorRepository = professorRepository;
    }

    public async Task<IReadOnlyCollection<SubjectDto>> Handle(GetStudentSubjectsQuery query, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetByIdAsync(new StudentId(query.StudentId), cancellationToken)
            ?? throw new NotFoundException(nameof(Student), query.StudentId);

        var subjectDtos = new List<SubjectDto>();
        foreach (var enrollment in student.Enrollments)
        {
            var subject = await _subjectRepository.GetByIdAsync(enrollment.SubjectId, cancellationToken);
            if (subject is null)
            {
                continue;
            }

            var professor = await _professorRepository.GetByIdAsync(subject.ProfessorId, cancellationToken);
            if (professor is null)
            {
                continue;
            }

            subjectDtos.Add(subject.ToDto(professor));
        }

        return subjectDtos;
    }
}
