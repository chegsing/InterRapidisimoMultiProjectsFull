using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Enrollment.RemoveSubject;

public sealed class RemoveSubjectHandler : ICommandHandler<RemoveSubjectCommand, Unit>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSubjectHandler(IStudentRepository studentRepository, IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveSubjectCommand command, CancellationToken cancellationToken = default)
    {
        var studentId = new StudentId(command.StudentId);
        var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken)
            ?? throw new NotFoundException(nameof(Student), command.StudentId);

        var subjectId = new SubjectId(command.SubjectId);
        student.Withdraw(subjectId);

        await _enrollmentRepository.DeleteAsync(studentId, subjectId, cancellationToken);
        await _studentRepository.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
