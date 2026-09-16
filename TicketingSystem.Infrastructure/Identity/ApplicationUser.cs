using Microsoft.AspNetCore.Identity;
using TicketingSystem.Application.Abstractions.Identity;

namespace TicketingSystem.Infrastructure.Identity;

public sealed class ApplicationUser:IdentityUser<Guid>,IApplicationUser
{
    public Guid? TenantId { get; private set; }
    public bool IsActive { get; private set; } = true;

    public void Activate()=> IsActive = true;
    public void Deactivate() => IsActive = false;
    public void SetTenant(Guid tenantId)=>TenantId = tenantId;
}
