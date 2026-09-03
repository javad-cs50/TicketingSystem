using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Abstractions.Persistence;

public interface INotificationRepository
{
    Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(Notification notification, CancellationToken cancellationToken);
}
