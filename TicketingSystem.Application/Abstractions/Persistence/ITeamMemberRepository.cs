using TicketingSystem.Application.Features.TeamMembers.DTOs;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Abstractions.Persistence;

public interface ITeamMemberRepository
{
    Task<bool> ExistsAsync(Guid teamId, Guid userId, CancellationToken cancellationToken);

    Task<IReadOnlyList<TeamMemberResponse>> GetByTeamIdAsync(Guid teamId, CancellationToken cancellationToken);
    Task AddAsync(TeamMember teamMember, CancellationToken cancellationToken);

    Task<int> RemoveAsync(Guid teamId, Guid userId, CancellationToken cancellationToken);
}
