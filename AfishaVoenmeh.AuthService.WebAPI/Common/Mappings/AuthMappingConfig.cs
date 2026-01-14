using AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;
using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using AfishaVoenmeh.AuthService.Application.Authentication.Queries.Login;
using AfishaVoenmeh.AuthService.Contracts.Requests;
using AfishaVoenmeh.AuthService.Contracts.Responses;
using Mapster;

namespace AfishaVoenmeh.AuthService.WebAPI.Common.Mappings;

public class AuthMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterUserRequest, RegisterUserCommand>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>();

        config.NewConfig<LoginUserRequest, LoginUserQuery>();
    }
}
