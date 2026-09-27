using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Nombre de una persona separado en nombre y apellido, tal como se almacena en las columnas
/// FirstName / LastName de dbo.Students y dbo.Professors.
/// </summary>
public sealed record PersonName
{
    public string FirstName { get; }
    public string LastName { get; }

    private PersonName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static PersonName Create(string firstName, string lastName)
    {
        var trimmedFirstName = Guard.AgainstNullOrWhiteSpace(firstName, nameof(FirstName), m => new InvalidPersonNameException(m));
        Guard.AgainstLengthGreaterThan(trimmedFirstName, DomainConstants.FirstNameMaxLength, nameof(FirstName), m => new InvalidPersonNameException(m));

        var trimmedLastName = Guard.AgainstNullOrWhiteSpace(lastName, nameof(LastName), m => new InvalidPersonNameException(m));
        Guard.AgainstLengthGreaterThan(trimmedLastName, DomainConstants.LastNameMaxLength, nameof(LastName), m => new InvalidPersonNameException(m));

        return new PersonName(trimmedFirstName, trimmedLastName);
    }

    public string FullName => $"{FirstName} {LastName}";

    public override string ToString() => FullName;
}
