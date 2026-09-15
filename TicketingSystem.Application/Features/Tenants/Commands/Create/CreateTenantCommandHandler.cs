using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Tenants.DTOs;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Features.Tenants.Commands.Create;

public sealed class CreateTenantCommandHandler(ITenantRepository tenantRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTenantCommand, CreateTenantResponse>
{
    public async Task<CreateTenantResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenantEntity = new Tenant(request.Name, request.Slug);
        await tenantRepository.AddAsync(tenantEntity,cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateTenantResponse(tenantEntity.Id);
    }
}
