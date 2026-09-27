using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.DeactivateStudent;

public sealed class DeactivateStudentHandler : ICommandHandler<DeactivateStudentCommand, Unit>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateStudentHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeactivateStudentCommand command, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetByIdAsync(new StudentId(command.StudentId), cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Student), command.StudentId);

        student.Deactivate();

        await _studentRepository.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
