namespace InterRapidisimoBackMultiProject.Application.Exceptions;

/// <summary>Se lanza cuando un caso de uso solicita una entidad que no existe.</summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException(string entityName, object key)
        : base($"'{entityName}' con identificador '{key}' no fue encontrado.")
    {
    }
}
