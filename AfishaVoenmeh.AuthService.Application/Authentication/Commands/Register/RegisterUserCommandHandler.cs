using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using AfishaVoenmeh.AuthService.Application.Common.Errors;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Authentication;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Persistence;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Services;
using AfishaVoenmeh.AuthService.Domain.UserAggregate;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.Entities;
using AfishaVoenmeh.AuthService.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

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
            return AuthenticationErrors.DublicateEmail;

        if (command.Password != command.PasswordConfirmation)
            return AuthenticationErrors.PasswordConfirmationFailed;

        var userCreds = UserCredentials.Create(command.FirstName, command.LastName, command.Patronymic);

        var userEmail = Email.Create(command.Email);
        if (userEmail.IsError)
            return userEmail.FirstError;

        var userPhoneNumber = PhoneNumber.Create(command.PhoneNumber);

        var hashedPassword = _passwordHasher.HashPassword(command.Password);
        var userPasswordHash = PasswordHash.Create(hashedPassword);

        var newUser = new User(userCreds, userEmail.Value, userPhoneNumber, userPasswordHash);
        newUser.ApplyRole(Role.Student);

        await _userRepository.AddAsync(newUser, cancellationToken);

        var token = _tokenGenerator.GenerateToken(newUser);

        return MapToAuthResult(newUser, token);
    }

    private static AuthenticationResult MapToAuthResult(User newUser, string token)
    {
        return new AuthenticationResult(
                    newUser.Id.Value,
                    newUser.Credentials.FirstName,
                    newUser.Credentials.LastName,
                    newUser.Credentials.Patronymic,
                    token);
    }
}