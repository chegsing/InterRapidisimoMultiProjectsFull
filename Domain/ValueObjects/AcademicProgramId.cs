namespace Domain.ValueObjects;

/// <summary>Identificador de dbo.AcademicPrograms.Id (INT IDENTITY).</summary>
public readonly record struct AcademicProgramId(int Value)
{
    public static readonly AcademicProgramId Unassigned = new(0);

    public bool IsTransient => Value == 0;

    public override string ToString() => Value.ToString();
}
