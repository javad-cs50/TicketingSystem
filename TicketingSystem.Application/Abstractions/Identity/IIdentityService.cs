namespace TicketingSystem.Application.Abstractions.Identity;

public interface IIdentityService
{
    Task<bool> CheckPasswordAsync(string email, string password, CancellationToken cancellationToken);

    Task<Guid?> FindUserIdByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Guid?> FindTenantIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<string>> GetRolesAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken cancellationToken);
}
