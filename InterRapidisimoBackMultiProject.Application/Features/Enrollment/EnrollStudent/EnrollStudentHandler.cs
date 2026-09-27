using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.Exceptions;

namespace InterRapidisimoBackMultiProject.Application.Features.Enrollment.EnrollStudent;

public sealed class EnrollStudentHandler : ICommandHandler<EnrollStudentCommand, Unit>
{
    private readonly IStudentRepository _studentRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IValidator<EnrollStudentCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollStudentHandler(
        IStudentRepository studentRepository,
        ISubjectRepository subjectRepository,
        IEnrollmentRepository enrollmentRepository,
        IValidator<EnrollStudentCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _subjectRepository = subjectRepository;
        _enrollmentRepository = enrollmentRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(EnrollStudentCommand command, CancellationToken cancellationToken = default)
    {
        var errors = await _validator.ValidateAsync(command, cancellationToken);
        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        var studentId = new StudentId(command.StudentId);
        var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken)
            ?? throw new NotFoundException(nameof(Student), command.StudentId);

        var subject = await _subjectRepository.GetByIdAsync(new SubjectId(command.SubjectId), cancellationToken)
            ?? throw new NotFoundException(nameof(Subject), command.SubjectId);

        var currentlyEnrolledSubjects = new List<Subject>();
        foreach (var enrollment in student.Enrollments)
        {
            var enrolledSubject = await _subjectRepository.GetByIdAsync(enrollment.SubjectId, cancellationToken);
            if (enrolledSubject is not null)
            {
                currentlyEnrolledSubjects.Add(enrolledSubject);
            }
        }

        var newEnrollment = student.EnrollIn(subject, currentlyEnrolledSubjects);

        await _enrollmentRepository.AddAsync(newEnrollment, cancellationToken);
        await _studentRepository.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
