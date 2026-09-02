using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;

public sealed class Ticket : BaseEntity
{
    private const int MaxTicketNumberLength = 50;
    private const int MaxTitleLength = 200;
    private const int MaxDescriptionLength = 10_000;

    private readonly List<Comment> _comments = [];

    public Guid TenantId { get; private set; }

    public string TicketNumber { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public Guid CustomerId { get; private set; }

    public Guid? TeamId { get; private set; }
    public Guid? AgentId { get; private set; }
    public Guid? CategoryId { get; private set; }

    public TicketStatus Status { get; private set; }
    public TicketPriority Priority { get; private set; }

    public DateTime? ResolvedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }

    public IReadOnlyCollection<Comment> Comments =>
        _comments.AsReadOnly();

    private Ticket(){}
    public Ticket(Guid tenantId, string ticketNumber, string title, string description, Guid customerId, Guid? categoryId = null)
    {
        if (tenantId == Guid.Empty)
            throw new DomainException(
                "Tenant ID cannot be empty.");

        if (customerId == Guid.Empty)
            throw new DomainException(
                "Customer ID cannot be empty.");

        TenantId = tenantId;
        CustomerId = customerId;
        CategoryId = categoryId;

        SetTicketNumber(ticketNumber);
        SetTitle(title);
        SetDescription(description);

        Status = TicketStatus.Open;
        Priority = TicketPriority.Medium;
    }

    public void ChangeTitle(string title)
    {
        SetTitle(title);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeDescription(string description)
    {
        SetDescription(description);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignToTeam(Guid teamId)
    {
        if (teamId == Guid.Empty)
            throw new DomainException(
                "Team ID cannot be empty.");

        TeamId = teamId;
        AgentId = null;

        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignToAgent(Guid agentId)
    {
        if (agentId == Guid.Empty)
            throw new DomainException(
                "Agent ID cannot be empty.");

        AgentId = agentId;

        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeCategory(Guid? categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePriority(TicketPriority priority)
    {
        if (!Enum.IsDefined(priority))
            throw new DomainException(
                "Invalid ticket priority.");

        Priority = priority;
        UpdatedAt = DateTime.UtcNow;
    }

    public void StartProgress()
    {
        if (Status != TicketStatus.Open)
            throw new DomainException(
                "Only open tickets can be moved to in-progress.");

        Status = TicketStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void WaitForCustomer()
    {
        if (Status != TicketStatus.InProgress)
            throw new DomainException(
                "Only in-progress tickets can wait for customer.");

        Status = TicketStatus.WaitingForCustomer;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ResumeFromCustomer()
    {
        if (Status != TicketStatus.WaitingForCustomer)
            throw new DomainException(
                "Only tickets waiting for customer can resume.");

        Status = TicketStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Resolve()
    {
        if (Status != TicketStatus.InProgress)
            throw new DomainException(
                "Only in-progress tickets can be resolved.");

        Status = TicketStatus.Resolved;
        ResolvedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        if (Status != TicketStatus.Resolved)
            throw new DomainException(
                "Only resolved tickets can be closed.");

        Status = TicketStatus.Closed;
        ClosedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reopen()
    {
        if (Status != TicketStatus.Closed && Status != TicketStatus.Resolved)
        {
            throw new DomainException(
                "Only resolved or closed tickets can be reopened.");
        }
        Status = TicketStatus.Open;
        ResolvedAt = null;
        ClosedAt = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddComment(Comment comment)
    {
        ArgumentNullException.ThrowIfNull(comment);

        if (comment.TicketId != Id)
            throw new DomainException(
                "Comment does not belong to this ticket.");

        _comments.Add(comment);
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetTicketNumber(string ticketNumber)
    {
        if (string.IsNullOrWhiteSpace(ticketNumber))
            throw new DomainException("Ticket number cannot be empty.");

        if (ticketNumber.Length > MaxTicketNumberLength)
            throw new DomainException($"Ticket number cannot exceed {MaxTicketNumberLength} characters.");

        TicketNumber = ticketNumber.Trim();
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException(
                "Ticket title cannot be empty.");

        if (title.Length > MaxTitleLength)
            throw new DomainException($"Ticket title cannot exceed {MaxTitleLength} characters.");

        Title = title.Trim();
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Ticket description cannot be empty.");

        if (description.Length > MaxDescriptionLength)
            throw new DomainException($"Ticket description cannot exceed {MaxDescriptionLength} characters.");

        Description = description.Trim();
    }
}