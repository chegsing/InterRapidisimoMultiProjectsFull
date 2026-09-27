namespace InterRapidisimoBackMultiProject.Application.DTOs;

/// <summary>Materia inscrita por un estudiante, con el nombre del profesor ya resuelto.</summary>
public sealed record SubjectDto(int Id, string Code, string Name, int Credits, int ProfessorId, string ProfessorFullName);
