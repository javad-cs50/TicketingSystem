using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.TeamMembers.DTOs;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Repositories;

public sealed class TeamMemberRepository(ApplicationDbContext context) : ITeamMemberRepository
{
    public async Task<bool> ExistsAsync(Guid teamId, Guid userId, CancellationToken cancellationToken)
    {
        return await context.TeamMembers
            .AnyAsync(x => x.TeamId == teamId && x.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<TeamMemberResponse>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken)
    {
        return await (from member in context.TeamMembers
                     join user in context.Users
                     on member.UserId equals user.Id
                     where member.TeamId ==teamId
                     select new TeamMemberResponse(user.Id ,user.UserName,user.Email)
                     )
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TeamMember teamMember, CancellationToken cancellationToken)
    {
        await context.TeamMembers.AddAsync(teamMember, cancellationToken);
    }

    public async Task<int> RemoveAsync(Guid teamId, Guid userId, CancellationToken cancellationToken)
    {
       return await context.TeamMembers
            .Where(x => x.TeamId == teamId &&
                        x.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}