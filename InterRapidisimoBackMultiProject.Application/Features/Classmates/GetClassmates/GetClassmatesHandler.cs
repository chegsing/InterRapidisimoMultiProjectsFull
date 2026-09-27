using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Classmates.GetClassmates;

public sealed class GetClassmatesHandler : IQueryHandler<GetClassmatesQuery, IReadOnlyCollection<SubjectClassmatesDto>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IClassmateDirectoryService _classmateDirectoryService;

    public GetClassmatesHandler(
        IStudentRepository studentRepository,
        ISubjectRepository subjectRepository,
        IEnrollmentRepository enrollmentRepository,
        IClassmateDirectoryService classmateDirectoryService)
    {
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
        _enrollmentRepository = enrollmentRepository;
        _classmateDirectoryService = classmateDirectoryService;
    }

    public async Task<IReadOnlyCollection<SubjectClassmatesDto>> Handle(GetClassmatesQuery query, CancellationToken cancellationToken = default)
    {
        var studentId = new StudentId(query.StudentId);
        if (await _studentRepository.GetByIdAsync(studentId, cancellationToken) is null)
        {
            throw new NotFoundException(nameof(Student), query.StudentId);
        }

        var allEnrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);
        var allStudents = await _studentRepository.GetAllAsync(cancellationToken);
        var allSubjects = await _subjectRepository.GetAllAsync(cancellationToken);
        var subjectsById = allSubjects.ToDictionary(s => s.Id);

        var classmatesBySubject = _classmateDirectoryService.GetClassmatesBySubject(studentId, allEnrollments, allStudents);

        return classmatesBySubject
            .Where(kvp => subjectsById.ContainsKey(kvp.Key))
            .Select(kvp => new SubjectClassmatesDto(
                kvp.Key.Value,
                subjectsById[kvp.Key].Name.Value,
                kvp.Value.Select(entry => entry.ToClassmateDto()).ToList()))
            .ToList();
    }
}
