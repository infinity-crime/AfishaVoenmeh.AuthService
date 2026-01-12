using AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;
using AfishaVoenmeh.AuthService.Contracts.Requests;
using AfishaVoenmeh.AuthService.Contracts.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AfishaVoenmeh.AuthService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest registerRequest, CancellationToken ct)
    {
        var command = new RegisterUserCommand
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Patronymic = registerRequest.Patronymic,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.PhoneNumber,
            Password = registerRequest.Password,
            PasswordConfirmation = registerRequest.PasswordConfirmation
        };

        var authResult = await _sender.Send(command, ct);

        var response = new AuthenticationResponse(
            authResult.Value.Id,
            authResult.Value.FirstName,
            authResult.Value.LastName,
            authResult.Value.Patronymic,
            authResult.Value.Token);

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUserRequest loginRequest)
    {
        return Ok();
    }
}