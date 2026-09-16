using MediatR;
using TicketingSystem.Application.Abstractions.Authentication;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Authentication.DTOs;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Features.Authentication.Commands.Login;

public sealed class LoginCommandHandler
    (IIdentityService identityService,
    ITokenService tokenService ,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userId = await identityService.FindUserIdByEmailAsync(request.Email, cancellationToken);

        if (userId is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var passwordIsValid = await identityService.CheckPasswordAsync(request.Email, request.Password, cancellationToken);

        if (!passwordIsValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var tenantId = await identityService.FindTenantIdAsync(userId.Value, cancellationToken);
        var roles = await identityService.GetRolesAsync(userId.Value, cancellationToken);
        //create auth token
        var accessToken = tokenService.GenerateAccessToken(userId.Value, tenantId, roles);
        var refreshToken = tokenService.GenerateRefreshToken();
        var hashRefreshToken = tokenService.HashRefreshToken(refreshToken.Token);
        var refreshTokenEntity = new TicketingSystem.Domain.Entities.RefreshToken(userId.Value, hashRefreshToken, refreshToken.ExpireAt);
        //add to db
        await refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new LoginResponse(accessToken.Token, refreshToken.Token, accessToken.ExpiresAt,refreshToken.ExpireAt);
    }
}