namespace TicketingSystem.Application.Features.Users.DTOs;

public sealed record UserResponse(Guid Id , string UserName , string Email ,bool IsActive);
