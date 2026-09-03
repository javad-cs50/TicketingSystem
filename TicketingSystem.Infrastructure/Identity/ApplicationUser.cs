using Microsoft.AspNetCore.Identity;

namespace TicketingSystem.Infrastructure.Identity;

public sealed class ApplicationUser:IdentityUser<Guid>
{
    public Guid? TenantId { get; set; }
    public bool IsActive { get; set; }
}
