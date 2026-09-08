using Microsoft.AspNetCore.Identity;
using TicketingSystem.Application.Common.Authorization;

namespace TicketingSystem.Infrastructure.Identity;

public static class IdentitySeeder
{
    private static readonly string[] roles = [UserRoles.PlatformAdmin ,UserRoles.TenantAdmin, UserRoles.Agent, UserRoles.Customer ];

    public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        foreach (string role in roles)
        {
            if (await roleManager.RoleExistsAsync(role))
                continue;
            var result =await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            if(!result.Succeeded)
            {
                var errors = string.Join(", ",result.Errors.Select(er => er.Description));
                throw new InvalidOperationException($"Failed to create role '{role}': {errors}");
            }
        }
    }
}
