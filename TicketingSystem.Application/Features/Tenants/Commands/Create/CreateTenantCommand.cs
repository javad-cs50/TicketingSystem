using MediatR;
using TicketingSystem.Application.Features.Tenants.DTOs;

namespace TicketingSystem.Application.Features.Tenants.Commands.Create;

public sealed record CreateTenantCommand(string Name, string Slug):IRequest<CreateTenantResponse>;

