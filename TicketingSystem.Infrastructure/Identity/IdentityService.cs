using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Identity;

namespace TicketingSystem.Infrastructure.Identity;

public sealed class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
{
    public async Task<bool> CheckPasswordAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
            return false;

        return await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<Guid?> FindUserIdByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);

        return user?.Id;
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return false;

        return await userManager.IsInRoleAsync(user, role);
    }

    public async Task<Guid?> FindTenantIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var tenantId = await userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => u.TenantId)
            .FirstOrDefaultAsync(cancellationToken);
        return tenantId;
    }
    

    public async Task<IReadOnlyCollection<string>> GetRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user =await userManager
            .FindByIdAsync(userId.ToString());
        if (user == null)
            return Array.Empty<string>();

        var roles = await userManager
            .GetRolesAsync(user);
        return (IReadOnlyCollection<string>)roles;
    }

    public async Task<(bool Succeeded, string[] Errors, Guid? UserId)> 
        CreateUserAsync(string email, string password, Guid? tenantId = null, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            TenantId = tenantId,
            IsActive = true,
        };
        var result =await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Select(e=>e.Description)
                .ToArray();
            return (false,errors,null );
        }
        return (true, Array.Empty<string>(), user.Id);
    }
}