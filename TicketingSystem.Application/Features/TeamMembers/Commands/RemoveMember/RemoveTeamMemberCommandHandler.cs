using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.TeamMembers.Commands.RemoveMember;

public sealed class RemoveTeamMemberCommandHandler
    (ITeamRepository teamRepository, ITeamMemberRepository teamMemberRepository, ICurrentUserService currentUserService)
    : IRequestHandler<RemoveTeamMemberCommand, int>
{
    public async Task<int> Handle(RemoveTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId;

        var team = await teamRepository.GetByIdAsync(request.TeamId, tenantId!.Value, cancellationToken);

        if (team is null)
            throw new KeyNotFoundException("Team not found.");

        if (team.TenantId != tenantId)
            throw new UnauthorizedAccessException("you can't change other tenants");

        var deletedRowsCount = await teamMemberRepository.RemoveAsync(request.TeamId, request.UserId, cancellationToken);
        if (deletedRowsCount == 0)
            throw new KeyNotFoundException("Team member not found.");

        return deletedRowsCount;
    }
}
