using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;

namespace InterRapidisimoBackMultiProject.Application.Features.Enrollment.GetStudentSubjects;

public sealed record GetStudentSubjectsQuery(int StudentId) : IQuery<IReadOnlyCollection<SubjectDto>>;
