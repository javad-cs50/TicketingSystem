namespace TicketingSystem.Application.Abstractions.Identity;

public interface IApplicationUser
{
    Guid Id { get; }
    string? UserName { get; }
    string? Email { get; }
    Guid? TenantId { get; }
    bool IsActive { get; }
    
    void SetTenant(Guid tenantId);
    void Activate();
    void Deactivate();
}
