using MediatR;
using TicketingSystem.Application.Features.Teams.DTOs;

namespace TicketingSystem.Application.Features.Teams.Commands.Create;

public sealed record CreateTeamCommand(string Name , string? Description) : IRequest<CreateTeamResponse>;
