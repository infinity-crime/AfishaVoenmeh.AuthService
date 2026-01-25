using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AfishaVoenmeh.AuthService.Infrastructure.Services;

public class RefreshSessionService : IRefreshSessionService
{
    private readonly IRefreshSessionRepository _refreshSessionRepository;
    private readonly JwtOptions _jwtOptions;

    public RefreshSessionService(
        IRefreshSessionRepository refreshSessionRepository, 
        IOptions<JwtOptions> jwtOptions)
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

    public async Task<RefreshSessionResult?> GetRefreshSessionAsync(string token, CancellationToken ct = default)
    {
        if(string.IsNullOrEmpty(token))
            return null;

        var refreshSession = await _refreshSessionRepository.GetByTokenAsync(token, ct);
        
        return refreshSession != null 
            ? new RefreshSessionResult(refreshSession.Token, refreshSession.ExpiresAt) 
            : null;
    }

    public async Task<RefreshSessionResult> CreateRefreshSessionAsync(UserId userId, string token, 
        CancellationToken ct = default)
    {
        var refreshSession = RefreshSession.Create(
            userId,
            token,
            DateTime.UtcNow.AddDays(_jwtOptions.ExpireRefreshToken));

        await _refreshSessionRepository.AddAsync(refreshSession, ct);

        return new RefreshSessionResult(
            refreshSession.Token,
            refreshSession.ExpiresAt);
    }
}
