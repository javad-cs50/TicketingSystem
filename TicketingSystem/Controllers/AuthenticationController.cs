using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Common;
using TicketingSystem.Application.Features.Authentication.Commands.Login;
using TicketingSystem.Application.Features.Authentication.DTOs;

namespace TicketingSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiVersion("1")]
[ApiController]
public class AuthenticationController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result =await sender.Send(command, cancellationToken);

        var response = new ApiResponse<LoginResponse>(result);
        return Ok(response);
    }
}

