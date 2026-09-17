using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Abstractions.Persistence;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(Guid id, Guid tenantId, CancellationToken cancellationToken);
    Task<Team?> GetByIdNoTrackAsync(Guid id, Guid tenantId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Team>?> GetListAsync(Guid tenantId, CancellationToken cancellationToken);
    Task AddAsync(Team team, CancellationToken cancellationToken);
}

