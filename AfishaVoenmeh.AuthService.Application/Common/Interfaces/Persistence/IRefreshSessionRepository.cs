using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;

namespace AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;

public interface IRefreshSessionRepository
{
    Task<RefreshSession?> GetByTokenAsync(string token, CancellationToken ct = default);
    Task AddAsync(RefreshSession refreshSession, CancellationToken ct = default);
    Task RemoveAsync(RefreshSession refreshSession, CancellationToken ct = default);
}
