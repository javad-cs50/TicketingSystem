using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;


public sealed class Notification : BaseEntity
{
    private const int MaxTitleLength = 200;
    private const int MaxMessageLength = 1000;

    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }

    public NotificationType Type { get; private set; }

    public string Title { get; private set; } = null!;
    public string Message { get; private set; } = null!;

    public bool IsRead { get; private set; }
    public DateTime? ReadAt { get; private set; }

    private Notification() { }

    public Notification(Guid tenantId, Guid userId, NotificationType type, string title, string message)
    {
        if (tenantId == Guid.Empty)
            throw new DomainException("Tenant ID cannot be empty.");

        if (userId == Guid.Empty)
            throw new DomainException("User ID cannot be empty.");

        TenantId = tenantId;
        UserId = userId;
        Type = type;

        SetTitle(title);
        SetMessage(message);

        IsRead = false;
    }

    public void MarkAsRead()
    {
        if (IsRead)
            return;

        IsRead = true;
        ReadAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException(
                "Notification title cannot be empty.");

        if (title.Length > MaxTitleLength)
            throw new DomainException(
                $"Notification title cannot exceed {MaxTitleLength} characters.");

        Title = title.Trim();
    }

    private void SetMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new DomainException(
                "Notification message cannot be empty.");

        if (message.Length > MaxMessageLength)
            throw new DomainException(
                $"Notification message cannot exceed {MaxMessageLength} characters.");

        Message = message.Trim();
    }
}
