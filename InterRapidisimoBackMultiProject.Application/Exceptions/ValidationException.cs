namespace InterRapidisimoBackMultiProject.Application.Exceptions;

/// <summary>Se lanza cuando un <see cref="Abstractions.IValidator{TRequest}"/> encuentra errores en el request.</summary>
public sealed class ValidationException : Exception
{
    public IReadOnlyCollection<string> Errors { get; }

    public ValidationException(IReadOnlyCollection<string> errors)
        : base("Se encontraron uno o más errores de validación.")
    {
        Errors = errors;
    }
}
