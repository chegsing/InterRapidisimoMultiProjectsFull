using Domain.Exceptions;

namespace Domain.Common;

/// <summary>
/// Clausulas de guarda reutilizables para validar invariantes de dominio.
/// </summary>
public static class Guard
{
    public static string AgainstNullOrWhiteSpace(string? value, string fieldName, Func<string, DomainException> exceptionFactory)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw exceptionFactory($"El campo '{fieldName}' es obligatorio.");
        }

        return value.Trim();
    }

    public static string AgainstLengthGreaterThan(string value, int maxLength, string fieldName, Func<string, DomainException> exceptionFactory)
    {
        if (value.Length > maxLength)
        {
            throw exceptionFactory($"El campo '{fieldName}' no puede superar {maxLength} caracteres.");
        }

        return value;
    }

    public static int AgainstNotPositive(int value, string fieldName, Func<string, DomainException> exceptionFactory)
    {
        if (value <= 0)
        {
            throw exceptionFactory($"El campo '{fieldName}' debe ser mayor a cero.");
        }

        return value;
    }
}
