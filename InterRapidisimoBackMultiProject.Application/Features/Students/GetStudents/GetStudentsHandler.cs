using Domain.Repositories;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.GetStudents;

public sealed class GetStudentsHandler : IQueryHandler<GetStudentsQuery, IReadOnlyCollection<StudentSummaryDto>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IReadOnlyCollection<StudentSummaryDto>> Handle(GetStudentsQuery query, CancellationToken cancellationToken = default)
    {
        var students = await _studentRepository.GetAllAsync(cancellationToken);
        return students.Select(s => s.ToSummaryDto()).ToList();
    }
}
