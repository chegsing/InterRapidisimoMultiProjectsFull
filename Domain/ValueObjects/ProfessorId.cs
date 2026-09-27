namespace Domain.ValueObjects;

/// <summary>Identificador de dbo.Professors.Id (INT IDENTITY).</summary>
public readonly record struct ProfessorId(int Value)
{
    public static readonly ProfessorId Unassigned = new(0);

    public bool IsTransient => Value == 0;

    public override string ToString() => Value.ToString();
}
