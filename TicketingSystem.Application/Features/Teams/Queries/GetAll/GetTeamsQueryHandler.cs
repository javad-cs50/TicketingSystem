using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Teams.DTOs;

namespace TicketingSystem.Application.Features.Teams.Queries.GetAll;

public sealed class GetTeamsQueryHandler
    (ITeamRepository teamRepository, ICurrentUserService currentUserService)
    : IRequestHandler<GetTeamsQuery, IReadOnlyList<TeamResponse>?>
{
    public async Task<IReadOnlyList<TeamResponse>?> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant context is missing.");

        var teams = await teamRepository.GetListAsync(tenantId, cancellationToken);
        if (teams == null)
            return [];

        return teams.Select(team => new TeamResponse(team.Id, team.TenantId, team.Name)).ToList();
    }
}