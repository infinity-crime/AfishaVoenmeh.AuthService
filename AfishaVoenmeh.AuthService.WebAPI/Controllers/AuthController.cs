using AfishaVoenmeh.AuthService.Contracts.Requests;
using AfishaVoenmeh.AuthService.Contracts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AfishaVoenmeh.AuthService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register(RegisterUserRequest registerRequest)
    {
        //var response = new AuthenticationResponse(
        //    Guid.NewGuid(),
        //    registerRequest.FirstName,
        //    registerRequest.LastName,
        //    registerRequest.Patronymic,
        //    "token...");

        //return Ok(response);

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUserRequest loginRequest)
    {
        return Ok();
    }
}