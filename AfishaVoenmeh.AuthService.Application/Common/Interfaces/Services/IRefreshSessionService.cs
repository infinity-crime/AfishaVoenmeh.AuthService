using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

namespace AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;

public interface IRefreshSessionService
{
    string GenerateRefreshToken();
    Task<RefreshSession> CreateRefreshSessionAsync(UserId userId, string token,
        CancellationToken ct = default);
    Task<RefreshSession> GetRefreshSessionAsync(string token, CancellationToken ct = default);
}
