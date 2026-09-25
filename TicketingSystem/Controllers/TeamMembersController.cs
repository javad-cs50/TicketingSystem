using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Common;
using TicketingSystem.Application.Features.TeamMembers.Commands.AddMember;
using TicketingSystem.Application.Features.TeamMembers.Commands.RemoveMember;
using TicketingSystem.Application.Features.TeamMembers.DTOs;
using TicketingSystem.Application.Features.TeamMembers.Queries.GetTeamMembers;

namespace TicketingSystem.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/teams/{teamId:guid}/members")]
public class TeamMembersController(ISender sender) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(Name ="GetTeamMembers")]
    public async Task<IActionResult> GetTeamMembers(Guid teamId , CancellationToken cancellationToken)
    {
        var res = await sender.Send(new GetTeamMembersQuery(teamId),cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<TeamMemberResponse>>(res));
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> AddMember(Guid teamId ,[FromBody]AddTeamMemberCommand req ,CancellationToken cancellationToken)
    {
        if (req.TeamId != teamId)
            return BadRequest("Team ID in route and body must match.");

        await sender.Send(req, cancellationToken);

        return CreatedAtRoute("GetTeamMembers", new {teamId });
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> RemoveMember(Guid teamId ,Guid userId , CancellationToken cancellationToken)
    {
        var deletedRows = await sender.Send(new RemoveTeamMemberCommand(teamId,userId),cancellationToken);
        return deletedRows > 0 ? NoContent() : NotFound();
    }
}
