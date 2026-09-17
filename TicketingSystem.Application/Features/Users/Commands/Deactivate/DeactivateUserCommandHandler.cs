using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.Users.Commands.Deactivate;

public sealed class DeactivateUserCommandHandler
    (IUserRepository userRepository, IUnitOfWork unitOfWork , ICurrentUserService currentUserService)
    : IRequestHandler<DeactivateUserCommand>
{
    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId;
        if (tenantId is null)
            throw new UnauthorizedAccessException("Tenant context is missing.");

        var userFromDb = await userRepository.GetByIdAsync(request.Id, tenantId.Value, cancellationToken);
        if (userFromDb is null)
            throw new KeyNotFoundException($"the id : {request.Id} not founded.");
        userFromDb.Deactivate();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
