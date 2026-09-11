using MediatR;
using TicketingSystem.Application.Abstractions.Authentication;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Features.Authentication.DTOs;

namespace TicketingSystem.Application.Features.Authentication.Commands.Login;

public sealed class LoginCommandHandler(IIdentityService identityService, ITokenService tokenService)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userId = await identityService.FindUserIdByEmailAsync(request.Email, cancellationToken);

        if (userId is null)
        {
            throw new UnauthorizedAccessException("email not exist.");
        }

        var passwordIsValid = await identityService.CheckPasswordAsync(request.Email, request.Password, cancellationToken);

        if (!passwordIsValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var tenantId = await identityService.FindTenantIdAsync(userId.Value, cancellationToken);
        var roles = await identityService.GetRolesAsync(userId.Value, cancellationToken);
        
        var accessToken = tokenService.GenerateAccessToken(userId.Value, tenantId, roles);
        var refreshToken = tokenService.GenerateRefreshToken();
        return new LoginResponse(accessToken.Token, refreshToken, accessToken.ExpiresAt);
    }
}