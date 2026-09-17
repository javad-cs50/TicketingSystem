using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Teams.DTOs;

namespace TicketingSystem.Application.Features.Teams.Queries.GetById;

public sealed class GetTeamByIdQueryHandler
    (ITeamRepository teamRepository, ICurrentUserService currentUserService)
    : IRequestHandler<GetTeamByIdQuery, TeamResponse?>
{
    public async Task<TeamResponse?> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant context is missing.");

        var team = await teamRepository.GetByIdNoTrackAsync(request.TeamId, cancellationToken);

        if (team is null)
            return null;

        return new TeamResponse(team.Id, team.TenantId, team.Name);
    }
}