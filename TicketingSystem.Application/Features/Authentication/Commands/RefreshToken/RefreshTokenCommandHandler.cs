using MediatR;
using TicketingSystem.Application.Abstractions.Authentication;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Authentication.DTOs;


namespace TicketingSystem.Application.Features.Authentication.Commands.RefreshToken;

public sealed record RefreshTokenCommandHandler
    (
    ITokenService TokenService,
    IIdentityService IdentityService,
    IRefreshTokenRepository RefreshTokenRepository,
    IUnitOfWork UnitOfWork
    )
    : IRequestHandler<RefreshTokenCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        //check if reffresh token exist and valid
        var hashRefreshToken = TokenService.HashRefreshToken(request.RefreshToken);
        var refreshTokenFromDb = await RefreshTokenRepository.GetByHashAsync(hashRefreshToken, cancellationToken);
        if (refreshTokenFromDb is null)
            throw new UnauthorizedAccessException("Invalid Refresh Token");
        if (refreshTokenFromDb.IsExpired)
            throw new UnauthorizedAccessException("Refresh Token Has Expired");
        if (refreshTokenFromDb.IsRevoked)
            throw new UnauthorizedAccessException("Refresh Token Has been Revoked");

        //get userid and tenantid of current user to create new access token and refresh token
        var tenantUser = await IdentityService
            .FindTenantIdAsync(refreshTokenFromDb.UserId, cancellationToken);
        var roles = await IdentityService.GetRolesAsync(refreshTokenFromDb.UserId, cancellationToken);
        //create new token
        var accessToken = TokenService.GenerateAccessToken(refreshTokenFromDb.UserId, tenantUser, roles);
        var newRefreshToken = TokenService.GenerateRefreshToken();
        var hashNewRefreshToken = TokenService.HashRefreshToken(newRefreshToken.Token);
        //save hashed refresh token to db
        var refreshTokenForSavingToDb = new TicketingSystem.Domain.Entities.RefreshToken(refreshTokenFromDb.UserId, hashNewRefreshToken, newRefreshToken.ExpireAt);
        await RefreshTokenRepository.AddAsync(refreshTokenForSavingToDb, cancellationToken);
        //revoke the old refresh token and link it to the new one
        refreshTokenFromDb.Revoke(refreshTokenForSavingToDb.Id);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
        return new LoginResponse(accessToken.Token, newRefreshToken.Token, accessToken.ExpiresAt,newRefreshToken.ExpireAt);
    }
}
