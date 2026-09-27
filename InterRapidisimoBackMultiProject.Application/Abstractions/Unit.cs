namespace InterRapidisimoBackMultiProject.Application.Abstractions;

/// <summary>Representa la ausencia de un valor de retorno útil (equivalente a "void" pero usable como TResponse).</summary>
public readonly record struct Unit
{
    public static readonly Unit Value = new();
}
