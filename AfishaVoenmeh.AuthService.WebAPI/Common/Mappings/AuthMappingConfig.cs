using AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;
using AfishaVoenmeh.AuthService.Contracts.Requests;
using Mapster;

namespace AfishaVoenmeh.AuthService.WebAPI.Common.Mappings;

public class AuthMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterUserRequest, RegisterUserCommand>();
    }
}
