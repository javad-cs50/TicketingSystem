using MediatR;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Commands.Update;

public sealed record UpdateTenantCommand(Guid TenantId, string Name, string Slug) : IRequest<CreateTenantResponse>;
