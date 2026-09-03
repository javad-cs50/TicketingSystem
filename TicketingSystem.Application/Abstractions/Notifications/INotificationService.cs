using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Application.Abstractions.Notifications;

public interface INotificationService
{
    Task SendAsync(Guid tenantId, Guid userId, NotificationType type, string title, string message, CancellationToken cancellationToken);
}
