using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>Teléfono opcional del estudiante (dbo.Students.Phone, NULL permitido).</summary>
public sealed record Phone
{
    public string Value { get; }

    private Phone(string value) => Value = value;

    public static Phone? CreateOptional(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var trimmed = value.Trim();
        Guard.AgainstLengthGreaterThan(trimmed, DomainConstants.PhoneMaxLength, nameof(Phone), m => new InvalidPhoneException(m));

        return new Phone(trimmed);
    }

    public override string ToString() => Value;
}
