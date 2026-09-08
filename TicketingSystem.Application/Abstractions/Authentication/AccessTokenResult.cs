namespace TicketingSystem.Application.Abstractions.Authentication;
public sealed record AccessTokenResult(string Token, DateTime ExpiresAt);
