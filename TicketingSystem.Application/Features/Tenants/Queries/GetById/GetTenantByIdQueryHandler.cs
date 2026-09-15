using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Queries.GetById;

public sealed class GetTenantByIdQueryHandler(ITenantRepository tenantRepository) : IRequestHandler<GetTenantByIdQuery, GetTenant>
{
    public async Task<GetTenant> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
    {
        var tenantFromDb = await tenantRepository.GetByIdNoTrackedAsync(request.TenantId, cancellationToken);
        if (tenantFromDb is null)
            throw new KeyNotFoundException($"Tenant with id '{request.TenantId}' was not found.");

        return new GetTenant(tenantFromDb.Id, tenantFromDb.Name, tenantFromDb.Slug, tenantFromDb.IsActive);
    }
}
