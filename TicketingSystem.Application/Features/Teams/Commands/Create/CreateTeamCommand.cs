using MediatR;

namespace TicketingSystem.Application.Features.Teams.Commands.Create;

public sealed record CreateTeamCommand(string Name , string? Description) : IRequest<Guid>;
