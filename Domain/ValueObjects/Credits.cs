using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Cantidad de créditos que otorga una materia. Toda materia equivale exactamente a
/// <see cref="DomainConstants.CreditsPerSubject"/> créditos (CK_Subjects_Credits).
/// </summary>
public sealed record Credits
{
    public int Value { get; }

    private Credits(int value) => Value = value;

    public static readonly Credits Default = new(DomainConstants.CreditsPerSubject);

    public static Credits Create(int value)
    {
        Guard.AgainstNotPositive(value, nameof(Credits), m => new InvalidCreditsException(m));
        if (value != DomainConstants.CreditsPerSubject)
        {
            throw new InvalidCreditsException($"Toda materia debe equivaler a {DomainConstants.CreditsPerSubject} créditos.");
        }

        return new Credits(value);
    }

    public override string ToString() => Value.ToString();
}
