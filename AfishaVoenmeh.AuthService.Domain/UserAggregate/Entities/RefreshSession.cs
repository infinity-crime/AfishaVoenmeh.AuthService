using AfishaVoenmeh.AuthService.Domain.Common.Abstract;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

namespace AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;

public class RefreshSession : Entity<RefreshSessionId>
{
    public UserId UserId { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public bool IsActive => DateTime.UtcNow < ExpiresAt;

    private RefreshSession() { } // EF Core

    private RefreshSession(RefreshSessionId refreshSessionId, UserId userId, string token, DateTime expiresAt)
        : base(refreshSessionId)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        CreatedAt = DateTime.UtcNow;
    }

    public static RefreshSession Create(UserId userId, string token, DateTime expiresAt) =>
        new(RefreshSessionId.CreateUnique(), userId, token, expiresAt);
}
