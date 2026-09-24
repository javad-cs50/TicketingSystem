using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Abstractions.Persistenceک;

namespace TicketingSystem.Application.Features.TeamMembers.Commands.RemoveMember;

public sealed class RemoveTeamMemberCommandHandler
    (ITeamRepository teamRepository, ITeamMemberRepository teamMemberRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveTeamMemberCommand>
{
    public async Task Handle(RemoveTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId;

        var team = await teamRepository.GetByIdAsync(request.TeamId, tenantId!.Value, cancellationToken);

        if (team is null)
            throw new KeyNotFoundException("Team not found.");

        if (team.TenantId != tenantId)
            throw new UnauthorizedAccessException("you can't change other tenants");

        await teamMemberRepository.RemoveAsync(request.TeamId, request.UserId, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
