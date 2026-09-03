using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Abstractions.Persistence;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog auditLog, CancellationToken cancellationToken);
}
