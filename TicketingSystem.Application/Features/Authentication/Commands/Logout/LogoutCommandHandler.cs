using MediatR;
using TicketingSystem.Application.Abstractions.Authentication;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.Authentication.Commands.Logout;

public sealed record LogoutCommandHandler
    (IRefreshTokenRepository RefreshTokenRepository ,
    IUnitOfWork UnitOfWork ,
    ITokenService TokenService
    ) 
    : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var hashRefreshToken = TokenService.HashRefreshToken(request.RefreshToken);
        var refreshTokenFromDb = await RefreshTokenRepository.GetByHashAsync(hashRefreshToken,cancellationToken);
        if (refreshTokenFromDb ==null || refreshTokenFromDb.IsRevoked)
            return;
        refreshTokenFromDb.Revoke();

        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}