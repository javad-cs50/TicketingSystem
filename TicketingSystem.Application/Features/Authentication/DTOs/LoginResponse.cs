namespace TicketingSystem.Application.Features.Authentication.DTOs;

public sealed record LoginResponse(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt);
