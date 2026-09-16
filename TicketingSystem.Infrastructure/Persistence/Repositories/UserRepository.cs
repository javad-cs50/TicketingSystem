using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Infrastructure.Persistence.Repositories
{
    internal class UserRepository(ApplicationDbContext applicationDbContext) : IUserRepository
    {
        public async Task<IApplicationUser?> GetByIdAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken)
            => await applicationDbContext.Users
                .Where(u => u.TenantId == tenantId)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        public async Task<IApplicationUser?> GetByIdNoTrackAsync(Guid userId, Guid tenantId, CancellationToken cancellationToken)
            => await applicationDbContext.Users
                .AsNoTracking()
                .Where(u => u.TenantId == tenantId)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        public async Task<IReadOnlyList<IApplicationUser>?> GetListAsync(Guid tenantId, CancellationToken cancellationToken)
            => await applicationDbContext.Users
                .AsNoTracking()
                .Where(u => u.TenantId == tenantId)
                .Cast<IApplicationUser>()
                .ToListAsync(cancellationToken);

    }
}
