namespace InterRapidisimoBackMultiProject.Application.DTOs;

/// <summary>Compañeros de clase de un estudiante agrupados por la materia que comparten.</summary>
public sealed record SubjectClassmatesDto(int SubjectId, string SubjectName, IReadOnlyCollection<ClassmateDto> Classmates);
