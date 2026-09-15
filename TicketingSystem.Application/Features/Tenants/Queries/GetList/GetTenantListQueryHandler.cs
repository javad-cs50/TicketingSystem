using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Queries.GetList;

public sealed class GetTenantListQueryHandler(ITenantRepository TenantRepository) : IRequestHandler<GetTenantListQuery, IEnumerable<GetTenant>>
{
    public async Task<IEnumerable<GetTenant>> Handle(GetTenantListQuery request, CancellationToken cancellationToken)
    {
        var tenantListFromDb = await TenantRepository.GetListAsync(cancellationToken);
        var tenantListResponse = tenantListFromDb.Select(t => new GetTenant(t.Id, t.Name, t.Slug, t.IsActive));
        return tenantListResponse;
    }
}
