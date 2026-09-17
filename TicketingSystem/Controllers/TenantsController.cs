using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Common;
using TicketingSystem.Application.Features.Tenants.Commands.Activate;
using TicketingSystem.Application.Features.Tenants.Commands.Create;
using TicketingSystem.Application.Features.Tenants.Commands.Deactivate;
using TicketingSystem.Application.Features.Tenants.Commands.Update;
using TicketingSystem.Application.Features.Tenants.DTOs;
using TicketingSystem.Application.Features.Tenants.Queries.GetById;
using TicketingSystem.Application.Features.Tenants.Queries.GetList;

namespace TicketingSystem.Api.Controllers;

[Route("api/v{version:apiVersion}/tenants")]
[ApiVersion("1.0")]
[ApiController]
public sealed class TenantsController(ISender sender) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //empty list return with 200 status code.
    [HttpGet(Name = "GetTenants")]
    public async Task<IActionResult> GetTenants(CancellationToken cancellationToken)
    {
        var res = await sender.Send(new GetTenantListQuery(), cancellationToken);
        return Ok(new ApiResponse<IEnumerable<GetTenantResponse>>(res, null));
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}",Name ="GetTenant")]
    public async Task<IActionResult> GetTenant(Guid id, CancellationToken cancellationToken)
    {
        var res = await sender.Send(new GetTenantByIdQuery(id), cancellationToken);
        return Ok(new ApiResponse<GetTenantResponse>(res, null));
    }
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost(Name = "CreateTenant")]
    public async Task<IActionResult> CreateTenant([FromBody]CreateTenantCommand req,CancellationToken cancellationToken)
    {
        var res =await sender.Send(req, cancellationToken);
        return CreatedAtRoute("GetTenant",
            new {id = res.Id },
            new ApiResponse<CreateTenantResponse>(res,null));
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id:guid}/activate", Name ="ActivateTenant")]
    public async Task<IActionResult> ActivateTenant(Guid id,CancellationToken cancellationToken)
    {
        await sender.Send(new ActivateTenantCommand(id),cancellationToken);
        return NoContent();
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id:guid}/deactivate", Name = "DeactivateTenant")]
    public async Task<IActionResult> DeactivateTenant(Guid id,CancellationToken cancellationToken)
    {
        await sender.Send(new DeactivateTenantCommand(id),cancellationToken);
        return NoContent();
    }
   
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id:guid}",Name ="UpdateTenant")]
    public async Task<IActionResult> UpdateTenant(Guid id, [FromBody]UpdateTenantCommand req,CancellationToken cancellationToken)
    {
        req = req with { TenantId = id };
        await sender.Send(req, cancellationToken);

        return NoContent();
    }
}
