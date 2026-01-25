using AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;
using AfishaVoenmeh.AuthService.Application.Authentication.Common;
using AfishaVoenmeh.AuthService.Application.Authentication.Queries.Login;
using AfishaVoenmeh.AuthService.Contracts.Requests;
using AfishaVoenmeh.AuthService.Contracts.Responses;
using AfishaVoenmeh.AuthService.WebAPI.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfishaVoenmeh.AuthService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AuthController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest registerRequest, CancellationToken ct)
    {
        var command = _mapper.Map<RegisterUserCommand>(registerRequest);

        var authResult = await _sender.Send(command, ct);

        return authResult.Match(
            authResult => SuccessRegister(authResult), 
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest loginRequest, CancellationToken ct)
    {
        var query = _mapper.Map<LoginUserQuery>(loginRequest);

        var authResult = await _sender.Send(query, ct);

        return authResult.Match(
            authResult => SuccessLogin(authResult),
            errors => Problem(errors));
    }

    private CreatedResult SuccessRegister(AuthenticationResult authenticationResult)
    {
        Response.Cookies.Append("refresh-cookie", authenticationResult.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = authenticationResult.RefreshTokenExpiration
        });

        var response = _mapper.Map<AuthenticationResponse>(authenticationResult);

        return Created(HttpContext.Request.Path, response);
    }

    private OkObjectResult SuccessLogin(AuthenticationResult authenticationResult)
    {
        Response.Cookies.Append("refresh-cookie", authenticationResult.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = authenticationResult.RefreshTokenExpiration
        });

        var response = _mapper.Map<AuthenticationResponse>(authenticationResult);

        return Ok(response);
    }
}