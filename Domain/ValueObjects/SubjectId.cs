namespace Domain.ValueObjects;

/// <summary>Identificador de dbo.Subjects.Id (INT IDENTITY).</summary>
public readonly record struct SubjectId(int Value)
{
    public static readonly SubjectId Unassigned = new(0);

    public bool IsTransient => Value == 0;

    public override string ToString() => Value.ToString();
}
