namespace Domain.ValueObjects;

/// <summary>Identificador de dbo.Students.Id (INT IDENTITY).</summary>
public readonly record struct StudentId(int Value)
{
    public static readonly StudentId Unassigned = new(0);

    public bool IsTransient => Value == 0;

    public override string ToString() => Value.ToString();
}
