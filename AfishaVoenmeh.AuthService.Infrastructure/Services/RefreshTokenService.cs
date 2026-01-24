using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;
using AfishaVoenmeh.AuthService.Infrastructure.Authentication.Common;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AfishaVoenmeh.AuthService.Infrastructure.Services;

public class RefreshTokenService : IRefreshSessionService
{
    private readonly IRefreshSessionRepository _refreshSessionRepository;
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenService(IRefreshSessionRepository refreshSessionRepository, IOptions<JwtOptions> jwtOptions)
    {
        _refreshSessionRepository = refreshSessionRepository;
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }

    public Task<RefreshSession> GetRefreshSessionAsync(string token, CancellationToken ct = default)
    {
        // TODO: Implement method to retrieve refresh session by token
        throw new NotImplementedException();
    }

    public async Task<RefreshSession> CreateRefreshSessionAsync(UserId userId, string token, 
        CancellationToken ct = default)
    {
        var refreshSession = RefreshSession.Create(
            userId,
            token,
            DateTime.UtcNow.AddDays(_jwtOptions.ExpireRefreshToken));

        await _refreshSessionRepository.AddAsync(refreshSession, ct);

        return refreshSession;
    }
}
