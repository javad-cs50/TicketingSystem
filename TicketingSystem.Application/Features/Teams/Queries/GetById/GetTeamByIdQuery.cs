using MediatR;
using TicketingSystem.Application.Features.Teams.DTOs;

namespace TicketingSystem.Application.Features.Teams.Queries.GetById;

public sealed record GetTeamByIdQuery(Guid TeamId) : IRequest<TeamResponse?>;