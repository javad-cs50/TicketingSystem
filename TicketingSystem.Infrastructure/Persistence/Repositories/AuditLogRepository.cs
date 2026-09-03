using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Repositories;

public sealed class AuditLogRepository(
    ApplicationDbContext dbContext) : IAuditLogRepository
{
    public async Task AddAsync(
        AuditLog auditLog,
        CancellationToken cancellationToken)
    {
        await dbContext.AuditLogs.AddAsync(
            auditLog,
            cancellationToken);
    }
}