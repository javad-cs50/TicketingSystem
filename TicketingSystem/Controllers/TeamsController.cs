using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Common;
using TicketingSystem.Application.Features.Teams.Commands.Create;
using TicketingSystem.Application.Features.Teams.Commands.Update;
using TicketingSystem.Application.Features.Teams.DTOs;
using TicketingSystem.Application.Features.Teams.Queries.GetAll;
using TicketingSystem.Application.Features.Teams.Queries.GetById;

namespace TicketingSystem.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/teams")]
public class TeamsController(ISender sender) : ControllerBase
{

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //empty list return with 200 status code.
    [HttpGet(Name = "GetTeams")]
    public async Task<IActionResult> GetTeams(CancellationToken cancellationToken)
    {
        var res = await sender.Send(new GetTeamsQuery(), cancellationToken);
        var apiRes = new ApiResponse<IReadOnlyList<TeamResponse>>(res);
        return Ok(apiRes);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}",Name ="GetTeam")]
    public async Task<IActionResult> GetTeam(Guid id ,CancellationToken cancellationToken)
    {
        var res = await sender.Send(new GetTeamByIdQuery(id), cancellationToken);
        var apiRes = new ApiResponse<TeamResponse>(res);
        return Ok(apiRes);
    }


    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost(Name ="CreateTeam")]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand req,CancellationToken cancellationToken)
    {
        var res = await sender.Send(req, cancellationToken);
        var apiRes = new ApiResponse<CreateTeamResponse>(res);
        return CreatedAtRoute("GetTeam", new { id = res.Id }, apiRes);
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id:guid}",Name ="UpdateTeam")]
    public async Task<IActionResult> UpdateTeam(Guid id , [FromBody] UpdateTeamCommand req ,CancellationToken cancellationToken)
    {
        await sender.Send(req, cancellationToken);
        return NoContent();
    }
}
