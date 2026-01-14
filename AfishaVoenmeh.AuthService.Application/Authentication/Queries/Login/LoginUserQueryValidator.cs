using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Queries.Login;

public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(q => q.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(q => q.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(5)
            .WithMessage("The minimum password length is 5.");
    }
}