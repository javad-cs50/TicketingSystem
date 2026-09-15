using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Commands.Update;

public sealed class UpdateTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTenantCommand, CreateTenantResponse>
{
    public async Task<CreateTenantResponse> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantFromDb =await tenantRepository.GetByIdAsync(request.TenantId, cancellationToken);
        if (tenantFromDb is null)
            throw new KeyNotFoundException($"Tenant with id '{request.TenantId}' was not found.");
        
        tenantFromDb.Update(request.Name, request.Slug);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateTenantResponse(tenantFromDb.Id);
    }
}
