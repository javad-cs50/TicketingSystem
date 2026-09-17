using MediatR;
using TicketingSystem.Application.Features.Teams.DTOs;

namespace TicketingSystem.Application.Features.Teams.Queries.GetAll;

public sealed record GetTeamsQuery : IRequest<IReadOnlyList<TeamResponse>?>;