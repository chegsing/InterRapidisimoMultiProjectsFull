using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.UpdateStudent;

public sealed class UpdateStudentHandler : ICommandHandler<UpdateStudentCommand, Unit>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IValidator<UpdateStudentCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudentHandler(IStudentRepository studentRepository, IValidator<UpdateStudentCommand> validator, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateStudentCommand command, CancellationToken cancellationToken = default)
    {
        var errors = await _validator.ValidateAsync(command, cancellationToken);
        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        var student = await _studentRepository.GetByIdAsync(new StudentId(command.StudentId), cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Student), command.StudentId);

        student.UpdateProfile(
            PersonName.Create(command.FirstName, command.LastName),
            Email.Create(command.Email),
            Phone.CreateOptional(command.Phone),
            new AcademicProgramId(command.AcademicProgramId));

        await _studentRepository.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
