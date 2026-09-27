namespace Domain.Common;

/// <summary>
/// Reglas de negocio y restricciones de esquema (dbo.AcademicPrograms, dbo.Professors, dbo.Students,
/// dbo.Subjects, dbo.StudentSubjects) centralizadas para evitar valores hardcoded.
/// </summary>
public static class DomainConstants
{
    public const int TotalSubjects = 10;
    public const int TotalProfessors = 5;
    public const int CreditsPerSubject = 3;
    public const int MaxSubjectsPerStudent = 3;
    public const int MaxSubjectsPerProfessor = 2;

    public const int IdentificationNumberMaxLength = 30;
    public const int FirstNameMaxLength = 100;
    public const int LastNameMaxLength = 100;
    public const int EmailMaxLength = 254;
    public const int PhoneMaxLength = 30;
    public const int SubjectCodeMaxLength = 30;
    public const int SubjectNameMaxLength = 150;
    public const int AcademicProgramNameMaxLength = 150;
    public const int AcademicProgramDescriptionMaxLength = 500;
}
