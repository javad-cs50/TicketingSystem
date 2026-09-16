using TicketingSystem.Application.Abstractions.Identity;

namespace TicketingSystem.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<IApplicationUser?> GetByIdAsync(Guid userId,Guid TenantId,CancellationToken cancellationToken);
    Task<IApplicationUser?> GetByIdNoTrackAsync(Guid userId,Guid TenantId,CancellationToken cancellationToken);
    Task<IReadOnlyList<IApplicationUser>?> GetListAsync(Guid TenantId, CancellationToken cancellationToken);


}
