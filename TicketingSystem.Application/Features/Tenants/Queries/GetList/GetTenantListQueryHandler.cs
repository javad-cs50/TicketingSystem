using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Queries.GetList;

public sealed class GetTenantListQueryHandler(ITenantRepository TenantRepository) : IRequestHandler<GetTenantListQuery, IEnumerable<GetTenantResponse>>
{
    public async Task<IEnumerable<GetTenantResponse>> Handle(GetTenantListQuery request, CancellationToken cancellationToken)
    {
        var tenantListFromDb = await TenantRepository.GetListAsync(cancellationToken);
        var tenantListResponse = tenantListFromDb.Select(t => new GetTenantResponse(t.Id, t.Name, t.Slug, t.IsActive));
        return tenantListResponse;
    }
}
