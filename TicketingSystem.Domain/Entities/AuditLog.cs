using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;

public sealed class AuditLog : BaseEntity
{
    private const int MaxEntityTypeLength = 100;

    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }

    public AuditAction Action { get; private set; }

    public string EntityType { get; private set; } = null!;
    public Guid EntityId { get; private set; }

    public string? Data { get; private set; }

    private AuditLog() { }

    public AuditLog(Guid tenantId, Guid userId, AuditAction action, string entityType, Guid entityId, string? data = null)
    {
        if (tenantId == Guid.Empty)
            throw new DomainException("Tenant ID cannot be empty.");

        if (userId == Guid.Empty)
            throw new DomainException("User ID cannot be empty.");

        if (entityId == Guid.Empty)
            throw new DomainException("Entity ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(entityType))
            throw new DomainException(
                "Entity type cannot be empty.");

        if (entityType.Length > MaxEntityTypeLength)
            throw new DomainException(
                $"Entity type cannot exceed {MaxEntityTypeLength} characters.");

        TenantId = tenantId;
        UserId = userId;
        Action = action;
        EntityType = entityType.Trim();
        EntityId = entityId;
        Data = data;
    }
}
