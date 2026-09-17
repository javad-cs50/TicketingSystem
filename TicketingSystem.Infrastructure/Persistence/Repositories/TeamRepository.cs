using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Repositories;

public sealed class TeamRepository(
    ApplicationDbContext dbContext) : ITeamRepository
{
    public async Task<Team?> GetByIdAsync(Guid id, Guid tenantId, CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId, cancellationToken);
    }

    public async Task<Team?> GetByIdNoTrackAsync(Guid id,Guid tenantId, CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .AsNoTracking()
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id && x.TenantId== tenantId, cancellationToken);
    }

    public async Task<IReadOnlyList<Team>?> GetListAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .AsNoTracking()
            .Where(x=>x.TenantId ==tenantId)
            .Include(x => x.Members)
            .ToListAsync( cancellationToken);
    }
    public async Task AddAsync(Team team, CancellationToken cancellationToken)
    {
        await dbContext.Teams.AddAsync(team, cancellationToken);
    }


}