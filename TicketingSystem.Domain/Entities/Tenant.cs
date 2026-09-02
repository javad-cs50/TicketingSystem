using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;

public sealed class Tenant : BaseEntity
{
    private const int MaxNameLength = 150;
    private const int MaxSlugLength = 100;

    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private Tenant() { }

    public Tenant(string name, string slug)
    {
        SetName(name);
        SetSlug(slug);

        IsActive = true;
    }

    public void Update(string name, string slug)
    {
        SetName(name);
        SetSlug(slug);

        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Tenant name cannot be empty.");

        if (name.Length > MaxNameLength)
            throw new DomainException(
                $"Tenant name cannot exceed {MaxNameLength} characters.");

        Name = name.Trim();
    }

    private void SetSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException("Tenant slug cannot be empty.");

        if (slug.Length > MaxSlugLength)
            throw new DomainException(
                $"Tenant slug cannot exceed {MaxSlugLength} characters.");

        Slug = slug.Trim().ToLowerInvariant();
    }
}