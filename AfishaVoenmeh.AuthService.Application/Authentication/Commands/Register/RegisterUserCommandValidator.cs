using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithMessage("First name cannot be empty.")
            .MinimumLength(2)
            .WithMessage("Too short first name.")
            .MaximumLength(30)
            .WithMessage("The name must be a maximum of 30 characters.");

        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithMessage("Last name cannot be empty.")
            .MinimumLength(2)
            .WithMessage("Too short last name.")
            .MaximumLength(40)
            .WithMessage("The last name must be a maximum of 40 characters.");

        RuleFor(c => c.Patronymic)
            .NotEmpty()
            .WithMessage("Patronymic cannot be empty.")
            .MinimumLength(2)
            .WithMessage("Too short patronymic.")
            .MaximumLength(40)
            .WithMessage("The patronymic must be a maximum of 40 characters.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number cannot be empty.")
            .Length(12)
            .WithMessage("The number must start with + and contain 11 digits.");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .MinimumLength(5)
            .WithMessage("The minimum password length is 5");
    }
}