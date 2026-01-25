using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using AfishaVoenmeh.AuthService.Application.Common.DTOs;
using AfishaVoenmeh.AuthService.Application.Common.Errors;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Authentication;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;
using AfishaVoenmeh.AuthService.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Queries.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IRefreshSessionService _refreshSessionService;

    public LoginUserQueryHandler(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshSessionService refreshSessionService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = jwtTokenGenerator;
        _refreshSessionService = refreshSessionService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(query.Email, cancellationToken);
        if (user is null)
            return AuthenticationErrors.EmailNotFound;

        if (!_passwordHasher.VerifyPassword(user.PasswordHash.Value, query.Password))
            return AuthenticationErrors.IncorrectPassword;

        var token = _tokenGenerator.GenerateAccessToken(user);

        var refreshToken = _refreshSessionService.GenerateRefreshToken();
        var refreshSession = await _refreshSessionService.CreateRefreshSessionAsync(
            user.Id, 
            refreshToken, 
            cancellationToken);

        return MapToAuthResult(user, token, refreshSession);
    }

    private ErrorOr<AuthenticationResult> MapToAuthResult(User user, string token, RefreshSessionResult result)
    {
        return new AuthenticationResult(
                    user.Id.Value,
                    user.Credentials.FirstName,
                    user.Credentials.LastName,
                    user.Credentials.Patronymic,
                    token,
                    result.Token,
                    result.ExpiresAt);
    }
}