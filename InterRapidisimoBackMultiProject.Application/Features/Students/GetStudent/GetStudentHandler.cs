using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.GetStudent;

public sealed class GetStudentHandler : IQueryHandler<GetStudentQuery, StudentDto>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto> Handle(GetStudentQuery query, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetByIdAsync(new StudentId(query.StudentId), cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Student), query.StudentId);

        return student.ToDto();
    }
}
