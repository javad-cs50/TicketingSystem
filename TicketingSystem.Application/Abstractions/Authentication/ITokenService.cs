namespace TicketingSystem.Application.Abstractions.Authentication;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, Guid? tenantId, IEnumerable<string> roles);
    string GenerateRefreshToken();
    string HashRefreshToken(string token);
}
