using MediatR;

namespace TicketingSystem.Application.Features.Tenants.Commands.Deactivate;

public sealed record DeactivateTenantCommand(Guid TenantId):IRequest
{
}