using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketingSystem.Application.Abstractions.Authentication;

namespace TicketingSystem.Infrastructure.Authentication.Jwt;

public sealed class JwtTokenService(IOptions<JwtOptions> options) : ITokenService
{
    private readonly JwtOptions _options = options.Value;

    public AccessTokenResult GenerateAccessToken(Guid userId, Guid? tenantId, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, userId.ToString())
    };

        if (tenantId.HasValue)
        {
            claims.Add(new Claim("tenant_id", tenantId.Value.ToString()));
        }

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(_options.AccessTokenExpirationMinutes);

        var token = new JwtSecurityToken
            (issuer: _options.Issuer, audience: _options.Audience, claims: claims, expires: expiresAt, signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AccessTokenResult(tokenString, expiresAt);
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