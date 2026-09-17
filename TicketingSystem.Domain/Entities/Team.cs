using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;

public sealed class Team : BaseEntity
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 500;

    private readonly List<TeamMember> _members = [];

    public Guid TenantId { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public IReadOnlyCollection<TeamMember> Members =>
        _members.AsReadOnly();

    private Team() { }

    public Team(
        Guid tenantId,
        string name,
        string? description = null)
    {
        if (tenantId == Guid.Empty)
            throw new DomainException("Tenant ID cannot be empty.");

        TenantId = tenantId;

        SetName(name);
        SetDescription(description);
    }

    public void Update(string? name, string? description)
    {
        this.Name = name ?? this.Name;
        this.Description = description ?? this.Description;

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddMember(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new DomainException("User ID cannot be empty.");

        if (_members.Any(x => x.UserId == userId))
            throw new DomainException(
                "User is already a member of this team.");

        _members.Add(new TeamMember(Id, userId));

        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveMember(Guid userId)
    {
        var member = _members.FirstOrDefault(x => x.UserId == userId);

        if (member is null)
            throw new DomainException(
                "User is not a member of this team.");

        _members.Remove(member);

        UpdatedAt = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Team name cannot be empty.");

        if (name.Length > MaxNameLength)
            throw new DomainException(
                $"Team name cannot exceed {MaxNameLength} characters.");

        Name = name.Trim();
    }

    private void SetDescription(string? description)
    {
        if (description is not null &&
            description.Length > MaxDescriptionLength)
        {
            throw new DomainException(
                $"Team description cannot exceed {MaxDescriptionLength} characters.");
        }

        Description = description?.Trim();
    }
}
