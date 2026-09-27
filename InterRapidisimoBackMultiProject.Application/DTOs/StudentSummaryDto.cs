namespace InterRapidisimoBackMultiProject.Application.DTOs;

/// <summary>
/// Vista pública y segura de un estudiante para que otros estudiantes consulten su registro en línea
/// (regla: no se exponen datos sensibles como correo, teléfono o identificación).
/// </summary>
public sealed record StudentSummaryDto(int Id, string FullName, int AcademicProgramId);
