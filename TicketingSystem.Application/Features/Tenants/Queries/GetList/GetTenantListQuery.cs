using MediatR;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Queries.GetList;

public sealed record GetTenantListQuery : IRequest<IEnumerable<GetTenant>>;