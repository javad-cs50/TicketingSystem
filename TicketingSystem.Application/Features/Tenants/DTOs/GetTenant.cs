namespace TicketingSystem.Application.Features.Tenants.DTOs;

public sealed record GetTenant(Guid Id, string Name, string Slug, bool IsActive);