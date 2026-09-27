namespace Domain.Common;

/// <summary>
/// Base para entidades con los campos de auditoría presentes en todas las tablas del esquema
/// (IsActive, CreatedAt, UpdatedAt).
/// </summary>
public abstract class AuditableEntity<TId> : Entity<TId>
    where TId : notnull
{
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }
    public bool IsActive { get; private set; }

    protected AuditableEntity(TId id, DateTime createdAtUtc, DateTime updatedAtUtc, bool isActive)
        : base(id)
    {
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
        IsActive = isActive;
    }

    protected void MarkAsUpdated() => UpdatedAtUtc = DateTime.UtcNow;

    public void Activate()
    {
        IsActive = true;
        MarkAsUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkAsUpdated();
    }
}
