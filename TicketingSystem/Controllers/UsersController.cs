using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Common;
using TicketingSystem.Application.Features.Users.Commands.Activate;
using TicketingSystem.Application.Features.Users.Commands.Deactivate;
using TicketingSystem.Application.Features.Users.Commands.Update;
using TicketingSystem.Application.Features.Users.DTOs;
using TicketingSystem.Application.Features.Users.Queries.GetAll;
using TicketingSystem.Application.Features.Users.Queries.GetById;

namespace TicketingSystem.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public sealed class UsersController(ISender sender) : ControllerBase
{

    [ProducesResponseType(typeof(IReadOnlyList<UserResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    // if collection of user was empty then 200OK will return.
    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var res = await sender.Send(new GetUsersQuery(), cancellationToken);

        var apiRes = new ApiResponse<IReadOnlyList<UserResponse>>(res);
        return Ok(apiRes);
    }

    [ProducesResponseType(typeof(ApiResponse<UserResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}",Name = "GetUserById")]
    public async Task<IActionResult> GetUserById(Guid id ,CancellationToken cancellationToken)
    {
        var res = await sender.Send(new GetUserByIdQuery(id), cancellationToken);
        if (res is null)
            return NotFound();

        var apiRes = new ApiResponse<UserResponse>(res);
        return Ok(apiRes);
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id:guid}/activate",Name = "ActivateUser")]
    public async Task<IActionResult> ActivateUser(Guid id,CancellationToken cancellationToken)
    {
        await sender.Send(new ActivateUserCommand(id), cancellationToken);
        return Ok();
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id:guid}/deactivate",Name = "DeactivateUser")]
    public async Task<IActionResult> DectivateUser(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeactivateUserCommand(id), cancellationToken);
        return Ok();
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id:guid}",Name ="UpdateUser")]
    public async Task<IActionResult> UpdateUser(Guid id , [FromBody]UpdateUserCommand req,CancellationToken cancellationToken )
    {
        req = req with { Id = id };
        await sender.Send(req, cancellationToken);
        return NoContent();
    }

}
