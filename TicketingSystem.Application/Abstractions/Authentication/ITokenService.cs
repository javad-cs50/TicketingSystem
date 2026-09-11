namespace TicketingSystem.Application.Abstractions.Authentication;

public interface ITokenService
{
    AccessTokenResult GenerateAccessToken(Guid userId, Guid? tenantId, IEnumerable<string> roles);
    RefreshTokenResult GenerateRefreshToken();
    string HashRefreshToken(string token);
}
