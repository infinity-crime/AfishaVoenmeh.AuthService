using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Authentication;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;
using AfishaVoenmeh.AuthService.Domain.UserAggregate;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;

    private readonly IJwtTokenGenerator _tokenGenerator;

    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IJwtTokenGenerator tokenGenerator,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByEmailAsync(command.Email, cancellationToken) != null)
            return Error.Conflict("User_Email_Exists", $"User with email {command.Email} already exists.");

        if (command.Password != command.PasswordConfirmation)
            return Error.Conflict("Password_Confirmation_Failed", "Password confirmation failed.");

        var userCreds = UserCredentials.Create(command.FirstName, command.LastName, command.Patronymic);
        if (userCreds.IsError)
            return userCreds.FirstError;

        var userEmail = Email.Create(command.Email);
        if (userEmail.IsError)
            return userEmail.FirstError;

        var userPhoneNumber = PhoneNumber.Create(command.PhoneNumber);
        if (userPhoneNumber.IsError)
            return userPhoneNumber.FirstError;

        var hashedPassword = _passwordHasher.HashPassword(command.Password);
        var userPasswordHash = PasswordHash.Create(hashedPassword);
        if(userPasswordHash.IsError)
            return userPasswordHash.FirstError;

        var newUser = new User(userCreds.Value, userEmail.Value, userPhoneNumber.Value, userPasswordHash.Value);

        await _userRepository.AddAsync(newUser, cancellationToken);

        var token = _tokenGenerator.GenerateToken(newUser);

        return new AuthenticationResult(
            newUser.Id.Value,
            newUser.Credentials.FirstName,
            newUser.Credentials.LastName,
            newUser.Credentials.Patronymic,
            token);
    }
}