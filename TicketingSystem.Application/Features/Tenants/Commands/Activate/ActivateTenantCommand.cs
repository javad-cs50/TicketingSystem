using MediatR;

namespace TicketingSystem.Application.Features.Tenants.Commands.Activate;

public sealed record ActivateTenantCommand(Guid TenantId) : IRequest;