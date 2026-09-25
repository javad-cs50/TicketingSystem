using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.TeamMembers.DTOs;

namespace TicketingSystem.Application.Features.TeamMembers.Queries.GetTeamMembers
{
    public sealed class GetTeamMembersQueryHandler
        (ICurrentUserService currentUserService, ITeamMemberRepository teamMemberRepository, ITeamRepository teamRepository)
        : IRequestHandler<GetTeamMembersQuery, IReadOnlyList<TeamMemberResponse>>
    {
        public async Task<IReadOnlyList<TeamMemberResponse>> Handle(GetTeamMembersQuery request, CancellationToken cancellationToken)
        {
            var tenantId = currentUserService.TenantId;
            if (tenantId is null)
                throw new UnauthorizedAccessException("Tenant context is missing");
            var team =await teamRepository.GetByIdNoTrackAsync(request.TeamId, tenantId!.Value, cancellationToken);
            if (team is null)
                throw new KeyNotFoundException("Team not found");

            return await teamMemberRepository.GetByTeamIdAsync(request.TeamId, cancellationToken);
            
        }
    }
}
