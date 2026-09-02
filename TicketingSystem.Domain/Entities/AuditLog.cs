using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Domain.Entities;

public sealed class AuditLog : BaseEntity
{
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }

    public AuditAction Action { get; private set; }

    public string EntityType { get; private set; } = null!;
    public Guid EntityId { get; private set; }

    public string? Data { get; private set; }

    private AuditLog() { }

    public AuditLog(
        Guid tenantId,
        Guid userId,
        AuditAction action,
        string entityType,
        Guid entityId,
        string? data = null)
    {
        TenantId = tenantId;
        UserId = userId;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        Data = data;
    }
}
