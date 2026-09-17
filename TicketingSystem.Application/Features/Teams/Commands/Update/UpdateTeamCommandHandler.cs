using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.Teams.Commands.Update;

public sealed class UpdateTeamCommandHandler
    (ITeamRepository teamRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IRequestHandler<UpdateTeamCommand>
{
    public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId ??
            throw new UnauthorizedAccessException("Tenant context is missing.");
        ;
        var teamFromDb = await teamRepository.GetByIdAsync(request.TeamId,tenantId,cancellationToken) ??
            throw new KeyNotFoundException($"There is no team with this Id :{request.TeamId} exsist.");
       
        teamFromDb.Update(request.Name, request.Description);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}