using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;

public sealed class Category : BaseEntity
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 500;

    public Guid TenantId { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    private Category() { }

    public Category(Guid tenantId, string name, string? description = null)
    {
        if (tenantId == Guid.Empty)
            throw new DomainException("Tenant ID cannot be empty.");

        TenantId = tenantId;

        SetName(name);
        SetDescription(description);

        IsActive = true;
    }

    public void Update(
        string name,
        string? description = null)
    {
        SetName(name);
        SetDescription(description);

        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty.");

        if (name.Length > MaxNameLength)
            throw new DomainException(
                $"Category name cannot exceed {MaxNameLength} characters.");

        Name = name.Trim();
    }

    private void SetDescription(string? description)
    {
        if (description is not null &&
            description.Length > MaxDescriptionLength)
        {
            throw new DomainException(
                $"Category description cannot exceed {MaxDescriptionLength} characters.");
        }

        Description = description?.Trim();
    }
}