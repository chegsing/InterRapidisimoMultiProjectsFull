using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>Número de identificación único (dbo.Students.IdentificationNumber / dbo.Professors.IdentificationNumber).</summary>
public sealed record IdentificationNumber
{
    public string Value { get; }

    private IdentificationNumber(string value) => Value = value;

    public static IdentificationNumber Create(string value)
    {
        var trimmed = Guard.AgainstNullOrWhiteSpace(value, nameof(IdentificationNumber), m => new InvalidIdentificationNumberException(m));
        Guard.AgainstLengthGreaterThan(trimmed, DomainConstants.IdentificationNumberMaxLength, nameof(IdentificationNumber), m => new InvalidIdentificationNumberException(m));

        return new IdentificationNumber(trimmed);
    }

    public override string ToString() => Value;
}
