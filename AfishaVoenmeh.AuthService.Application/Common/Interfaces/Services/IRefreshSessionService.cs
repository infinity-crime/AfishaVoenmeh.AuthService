using AfishaVoenmeh.AuthService.Application.Common.DTOs;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;

namespace AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;

public interface IRefreshSessionService
{
    string GenerateRefreshToken();
    Task<RefreshSessionResult> CreateRefreshSessionAsync(UserId userId, string token, CancellationToken ct = default);
    Task<RefreshSessionResult?> GetRefreshSessionAsync(string token, CancellationToken ct = default);
}
