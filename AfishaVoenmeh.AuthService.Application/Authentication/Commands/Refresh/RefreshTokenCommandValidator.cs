using FluentValidation;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Commands.Refresh;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(c => c.Token)
            .Length(256)
            .WithMessage("The refresh token has an invalid length.");
    }
}
