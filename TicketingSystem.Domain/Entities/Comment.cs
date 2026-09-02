using TicketingSystem.Domain.Common;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;

public sealed class Comment : BaseEntity
{
    private const int MaxContentLength = 5000;

    public Guid TicketId { get; private set; }
    public Guid AuthorId { get; private set; }
    public string Content { get; private set; } = null!;

    private Comment() { }

    internal Comment(Guid ticketId, Guid authorId, string content)
    {
        if (ticketId == Guid.Empty)
            throw new DomainException("Ticket ID cannot be empty.");

        if (authorId == Guid.Empty)
            throw new DomainException("Author ID cannot be empty.");

        SetContent(content);

        TicketId = ticketId;
        AuthorId = authorId;
    }

    public void UpdateContent(string content)
    {
        SetContent(content);
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException(
                "Comment content cannot be empty.");

        if (content.Length > MaxContentLength)
            throw new DomainException(
                $"Comment content cannot exceed {MaxContentLength} characters.");

        Content = content.Trim();
    }
}
