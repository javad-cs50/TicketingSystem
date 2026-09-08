using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketingSystem.Application.Abstractions.Identity;

namespace TicketingSystem.Infrastructure.Services;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User ?? new ClaimsPrincipal();
    public Guid? UserId
    {
        get
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var userId) ? userId : null;
        }
    }
    public Guid? TenantId
    {
        get
        {
            var value = User.FindFirstValue("tenant_id");
            return Guid.TryParse(value, out var tenantId) ? tenantId : null;
        }
    }
    public bool IsAuthenticated => User.Identity?.IsAuthenticated == true;
    public bool IsInRole(string role) => User.IsInRole(role);

}