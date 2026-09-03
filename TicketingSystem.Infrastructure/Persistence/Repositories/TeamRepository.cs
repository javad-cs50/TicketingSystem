using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Repositories;

public sealed class TeamRepository(
    ApplicationDbContext dbContext) : ITeamRepository
{
    public async Task<Team?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await dbContext.Teams
            .Include(x => x.Members)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        Team team,
        CancellationToken cancellationToken)
    {
        await dbContext.Teams.AddAsync(
            team,
            cancellationToken);
    }
}