using Microsoft.Extensions.Options;
using TicketingSystem.Application.Abstractions.Authentication;

namespace TicketingSystem.Infrastructure.Authentication.Jwt;

public sealed class JwtTokenService(IOptions<JwtOptions> options) : ITokenService
{
    private readonly JwtOptions _options = options.Value;

    public AccessTokenResult GenerateAccessToken(Guid userId, Guid? tenantId, IEnumerable<string> roles)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public string HashRefreshToken(string token)
    {
        throw new NotImplementedException();
    }
}