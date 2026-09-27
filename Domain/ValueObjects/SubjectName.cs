using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>
/// Nombre de una materia académica.
/// </summary>
public sealed record SubjectName
{
    public string Value { get; }

    private SubjectName(string value) => Value = value;

    public static SubjectName Create(string value)
    {
        var trimmed = Guard.AgainstNullOrWhiteSpace(value, nameof(SubjectName), m => new InvalidSubjectNameException(m));
        Guard.AgainstLengthGreaterThan(trimmed, DomainConstants.SubjectNameMaxLength, nameof(SubjectName), m => new InvalidSubjectNameException(m));

        return new SubjectName(trimmed);
    }

    public override string ToString() => Value;
}
