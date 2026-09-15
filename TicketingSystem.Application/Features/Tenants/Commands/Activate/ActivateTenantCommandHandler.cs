using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.Tenants.Commands.Activate;

public sealed class ActivateTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork) : IRequestHandler<ActivateTenantCommand>
{
    public async Task Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantFromDb=await tenantRepository.GetByIdAsync(request.TenantId,cancellationToken);
        if (tenantFromDb is null)
            throw new KeyNotFoundException($"Tenant with id '{request.TenantId}' was not found.");
        tenantFromDb.Activate();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}