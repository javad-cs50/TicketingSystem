namespace TicketingSystem.Application.Features.Tenants.DTOs;

public sealed record GetTenantResponse(Guid Id, string Name, string Slug, bool IsActive);