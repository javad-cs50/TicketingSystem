using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Persistenceک;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Repositories;

public sealed class TeamMemberRepository(ApplicationDbContext context) : ITeamMemberRepository
{
    public async Task<bool> ExistsAsync(Guid teamId, Guid userId, CancellationToken cancellationToken)
    {
        return await context.TeamMembers
            .AnyAsync(x => x.TeamId == teamId && x.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<TeamMember>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken)
    {
        return await context.TeamMembers
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TeamMember teamMember, CancellationToken cancellationToken)
    {
        await context.TeamMembers.AddAsync(teamMember, cancellationToken);
    }

    public async Task RemoveAsync(Guid teamId, Guid userId, CancellationToken cancellationToken)
    {
        await context.TeamMembers
            .Where(x => x.TeamId == teamId &&
                        x.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}