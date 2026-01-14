using AfishaVoenmeh.AuthService.Application.Authentication.Commands.Register;
using AfishaVoenmeh.AuthService.Contracts.Requests;
using AfishaVoenmeh.AuthService.Contracts.Responses;
using AfishaVoenmeh.AuthService.WebAPI.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AfishaVoenmeh.AuthService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
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
            authResult => Created(HttpContext.Request.Path, authResult), 
            errors => Problem(errors));
    }

    //[HttpPost("login")]
    //public IActionResult Login(LoginUserRequest loginRequest)
    //{
    //    return Ok();
    //}
}