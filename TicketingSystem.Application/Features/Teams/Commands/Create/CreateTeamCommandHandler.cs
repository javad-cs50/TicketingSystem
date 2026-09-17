using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Teams.DTOs;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Features.Teams.Commands.Create;

public sealed class CreateTeamCommandHandler
    (ITeamRepository teamRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : IRequestHandler<CreateTeamCommand,CreateTeamResponse>
{
    public async Task<CreateTeamResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant context is missing.");
      
        var teamToCreate = new Team(tenantId, request.Name, request.Description);
        await teamRepository.AddAsync(teamToCreate,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
       
        return new CreateTeamResponse( teamToCreate.Id);

    }
}
