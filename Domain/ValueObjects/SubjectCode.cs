using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

/// <summary>Código único de una materia (dbo.Subjects.Code).</summary>
public sealed record SubjectCode
{
    public string Value { get; }

    private SubjectCode(string value) => Value = value;

    public static SubjectCode Create(string value)
    {
        var trimmed = Guard.AgainstNullOrWhiteSpace(value, nameof(SubjectCode), m => new InvalidSubjectCodeException(m));
        Guard.AgainstLengthGreaterThan(trimmed, DomainConstants.SubjectCodeMaxLength, nameof(SubjectCode), m => new InvalidSubjectCodeException(m));

        return new SubjectCode(trimmed);
    }

    public override string ToString() => Value;
}
