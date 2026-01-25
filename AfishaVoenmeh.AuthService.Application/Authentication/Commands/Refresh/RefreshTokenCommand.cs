using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Commands.Refresh;

public record RefreshTokenCommand(string Token) : IRequest<ErrorOr<RefreshTokenResult>>;
