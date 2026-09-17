using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.Users.Commands.Activate;

public sealed class ActivateUserCommandHandler
    (IUserRepository userRepository, IUnitOfWork unitOfWork , ICurrentUserService currentUserService)
    : IRequestHandler<ActivateUserCommand>
{
    public async Task Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId;
        if (tenantId is null)
            throw new UnauthorizedAccessException("Tenant context is missing.");

        var userFromDb = await userRepository.GetByIdAsync(request.Id,tenantId.Value, cancellationToken);
        if (userFromDb is null)
            throw new KeyNotFoundException($"the id : {request.Id} not founded.");
        userFromDb.Activate();
        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
