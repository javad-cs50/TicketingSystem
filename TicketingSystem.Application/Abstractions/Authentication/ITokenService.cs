namespace TicketingSystem.Application.Abstractions.Authentication;

public interface ITokenService
{
    AccessTokenResult GenerateAccessToken(Guid userId, Guid? tenantId, IEnumerable<string> roles);
    string GenerateRefreshToken();
    string HashRefreshToken(string token);
}
