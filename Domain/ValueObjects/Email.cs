using System.Text.RegularExpressions;
using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Correo electrónico validado del estudiante o profesor.
/// </summary>
public sealed partial record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailException("El correo electrónico es obligatorio.");
        }

        var trimmed = value.Trim();
        if (!EmailRegex().IsMatch(trimmed))
        {
            throw new InvalidEmailException($"El correo electrónico '{trimmed}' no tiene un formato válido.");
        }

        Guard.AgainstLengthGreaterThan(trimmed, DomainConstants.EmailMaxLength, nameof(Email), m => new InvalidEmailException(m));

        return new Email(trimmed);
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}
