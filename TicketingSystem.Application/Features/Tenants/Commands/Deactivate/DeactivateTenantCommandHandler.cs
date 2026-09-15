using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.Tenants.Commands.Deactivate;

public sealed class DeactivateTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeactivateTenantCommand>
{
    public async Task Handle(DeactivateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantFromDb = await tenantRepository.GetByIdAsync(request.TenantId, cancellationToken);
        if (tenantFromDb is null)
            throw new KeyNotFoundException($"Tenant with id '{request.TenantId}' was not found.");
        tenantFromDb.Deactivate();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}