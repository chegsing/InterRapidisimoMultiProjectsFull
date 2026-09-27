using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>Nombre de un programa académico (dbo.AcademicPrograms.Name).</summary>
public sealed record AcademicProgramName
{
    public string Value { get; }

    private AcademicProgramName(string value) => Value = value;

    public static AcademicProgramName Create(string value)
    {
        var trimmed = Guard.AgainstNullOrWhiteSpace(value, nameof(AcademicProgramName), m => new InvalidAcademicProgramException(m));
        Guard.AgainstLengthGreaterThan(trimmed, DomainConstants.AcademicProgramNameMaxLength, nameof(AcademicProgramName), m => new InvalidAcademicProgramException(m));

        return new AcademicProgramName(trimmed);
    }

    public override string ToString() => Value;
}
