using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Repositories;

public sealed class RefreshTokenRepository(
ApplicationDbContext context)
: IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default)
    {
        return await context.RefreshTokens
            .SingleOrDefaultAsync(
                x => x.TokenHash == tokenHash,
                cancellationToken);
    }

    public async Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default)
    {
        await context.RefreshTokens.AddAsync(
            refreshToken,
            cancellationToken);
    }
}
