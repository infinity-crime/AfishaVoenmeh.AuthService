using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Commands.Refresh;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<RefreshTokenResult>>
{
    private readonly IRefreshSessionService _refreshSessionService;

    private readonly ILogger<RefreshTokenCommandHandler> _logger;

    public RefreshTokenCommandHandler(
        IRefreshSessionService refreshSessionService,
        ILogger<RefreshTokenCommandHandler> logger)
    {
        _refreshSessionService = refreshSessionService;
        _logger = logger;
    }

    public async Task<ErrorOr<RefreshTokenResult>> Handle(
        RefreshTokenCommand command, 
        CancellationToken cancellationToken)
    {
        var refreshSession = await _refreshSessionService
            .GetRefreshSessionAsync(command.Token, cancellationToken);

        if(refreshSession is null)
        {
            // TODO: Handle compromised token scenario

            _logger.LogWarning("Refresh session not found for token: {Token}!", command.Token);

            return Error.Unauthorized("", "");
        }

        var newAccessToken = "";

        var newRefreshToken = _refreshSessionService.GenerateRefreshToken();
        var newRefreshSession = await _refreshSessionService.CreateRefreshSessionAsync(
            refreshSession.UserId,
            newRefreshToken,
            cancellationToken);

        return new RefreshTokenResult(
            newAccessToken, 
            newRefreshToken, 
            newRefreshSession.ExpiresAt);
    }
}
