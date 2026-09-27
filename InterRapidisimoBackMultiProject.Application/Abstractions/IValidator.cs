namespace InterRapidisimoBackMultiProject.Application.Abstractions;

/// <summary>Valida un request de la Application (formato + reglas que requieren consultar puertos, p. ej. unicidad).</summary>
public interface IValidator<in TRequest>
{
    Task<IReadOnlyCollection<string>> ValidateAsync(TRequest request, CancellationToken cancellationToken = default);
}
