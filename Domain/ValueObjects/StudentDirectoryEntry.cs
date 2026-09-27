namespace Domain.ValueObjects;

/// <summary>
/// Proyección pública y segura de un estudiante: solo expone el nombre, nunca datos sensibles
/// como el correo, para cumplir la regla de visibilidad entre estudiantes.
/// </summary>
public sealed record StudentDirectoryEntry(StudentId StudentId, PersonName PersonName);
