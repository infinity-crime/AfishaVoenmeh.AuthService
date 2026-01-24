using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;

namespace AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;

public interface IRefreshSessionRepository
{
    Task<RefreshSession> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default);
    Task AddAsync(RefreshSession refreshSession, CancellationToken ct = default);
    Task RemoveAsync(Guid refreshSessionId, CancellationToken ct = default);
}
