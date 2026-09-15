using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Common;
using TicketingSystem.Application.Features.Authentication.Commands.Login;
using TicketingSystem.Application.Features.Authentication.Commands.RefreshToken;
using TicketingSystem.Application.Features.Authentication.Commands.Register;
using TicketingSystem.Application.Features.Authentication.DTOs;

namespace TicketingSystem.Api.Controllers;

[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
[ApiController]
public sealed class AuthenticationController(ISender sender) : ControllerBase
{

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        var response = new ApiResponse<LoginResponse>(result);
        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        var response = new ApiResponse<RegisterResponse>(result);
        //Creted() + response
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody]RefreshTokenCommand command,CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.RefreshToken))
            return Unauthorized();
        var commandResponse =await sender.Send(command, cancellationToken);

        return Ok(new ApiResponse<LoginResponse>(commandResponse));
    }

}

