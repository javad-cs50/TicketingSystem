using TicketingSystem.Domain.Common;

namespace TicketingSystem.Domain.Entities;

public sealed class RefreshToken : BaseEntity
{
    public Guid UserId { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public Guid? ReplacedByTokenId { get; private set; }
    public bool IsRevoked => RevokedAt.HasValue;
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    //to prevent creating invalid object
    private RefreshToken() { }
    public RefreshToken(Guid userId, string tokenHash, DateTime expiresAt)
    {
        UserId = userId;
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
    }

    public void Revoke()
    {
        if (IsRevoked)
            return;

        RevokedAt = DateTime.UtcNow;
    }
}
