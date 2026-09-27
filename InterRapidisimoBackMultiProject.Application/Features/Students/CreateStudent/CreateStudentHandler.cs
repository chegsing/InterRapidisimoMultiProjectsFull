using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.CreateStudent;

public sealed class CreateStudentHandler : ICommandHandler<CreateStudentCommand, int>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IValidator<CreateStudentCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentHandler(IStudentRepository studentRepository, IValidator<CreateStudentCommand> validator, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateStudentCommand command, CancellationToken cancellationToken = default)
    {
        var errors = await _validator.ValidateAsync(command, cancellationToken);
        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        var student = Student.Create(
            IdentificationNumber.Create(command.IdentificationNumber),
            PersonName.Create(command.FirstName, command.LastName),
            Email.Create(command.Email),
            Phone.CreateOptional(command.Phone),
            new AcademicProgramId(command.AcademicProgramId));

        await _studentRepository.AddAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return student.Id.Value;
    }
}
