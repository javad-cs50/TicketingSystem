namespace TicketingSystem.Application.Features.Teams.DTOs;

public sealed record TeamResponse(Guid Id, Guid TenantId, string Name);