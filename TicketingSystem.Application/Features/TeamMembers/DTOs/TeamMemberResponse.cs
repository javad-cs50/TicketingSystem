namespace TicketingSystem.Application.Features.TeamMembers.DTOs;

public sealed record TeamMemberResponse(Guid UserId, string? UserName, string? Email);