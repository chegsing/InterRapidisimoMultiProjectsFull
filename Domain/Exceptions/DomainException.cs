namespace Domain.Exceptions;

/// <summary>
/// Excepción base para toda violación de una regla de negocio del dominio académico.
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }
}
