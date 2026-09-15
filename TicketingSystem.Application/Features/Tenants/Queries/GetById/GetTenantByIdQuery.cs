using MediatR;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Queries.GetById;

public sealed record GetTenantByIdQuery(Guid TenantId) : IRequest<GetTenant>;