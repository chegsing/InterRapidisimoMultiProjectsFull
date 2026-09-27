using InterRapidisimoBackMultiProject.Application.Abstractions;
using InterRapidisimoBackMultiProject.Application.DTOs;

namespace InterRapidisimoBackMultiProject.Application.Features.Students.GetStudent;

public sealed record GetStudentQuery(int StudentId) : IQuery<StudentDto>;
