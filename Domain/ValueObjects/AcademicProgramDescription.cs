using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>Descripción opcional de un programa académico (dbo.AcademicPrograms.Description, NULL permitido).</summary>
public sealed record AcademicProgramDescription
{
    public string Value { get; }

    private AcademicProgramDescription(string value) => Value = value;

    public static AcademicProgramDescription? CreateOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var trimmed = value.Trim();
        Guard.AgainstLengthGreaterThan(trimmed, DomainConstants.AcademicProgramDescriptionMaxLength, nameof(AcademicProgramDescription), m => new InvalidAcademicProgramException(m));

        return new AcademicProgramDescription(trimmed);
    }

    public override string ToString() => Value;
}
