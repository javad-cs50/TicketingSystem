using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Abstractions.Persistenceک;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Features.TeamMembers.Commands.AddMember;

public sealed class AddTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository, ITeamRepository teamRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : IRequestHandler<AddTeamMemberCommand>
{
    public async Task Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId;
        var team = await teamRepository.GetByIdNoTrackAsync(request.TeamId, tenantId!.Value, cancellationToken);
        if (team is null)
            throw new KeyNotFoundException("Team not found.");

        var user = userRepository.GetByIdNoTrackAsync(request.UserId, tenantId!.Value, cancellationToken);
        if (user is null)
            throw new KeyNotFoundException("User not found.");

        var isExist = await teamMemberRepository.ExistsAsync(request.TeamId, request.UserId, cancellationToken);

        if (isExist)
            throw new InvalidOperationException("User is already a member of this team.");

        var teamMember = new TeamMember(request.TeamId,request.UserId);

        await teamMemberRepository.AddAsync(teamMember, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);


    }
}
