using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using AfishaVoenmeh.AuthService.Application.Common.Interfaces.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _tokenGenerator;

    public RegisterUserCommandHandler(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthenticationResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        // check if user email exists

        // check password confirmation

        // create user

        // registration

        var userId = Guid.NewGuid();

        var token = _tokenGenerator.GenerateToken(userId, command.FirstName, command.LastName, command.Patronymic, command.Email);

        return new AuthenticationResult(
            userId,
            command.FirstName,
            command.LastName,
            command.Patronymic,
            token);
    }
}