using MediatR;

namespace TicketingSystem.Application.Features.Teams.Commands.Update;

public sealed record UpdateTeamCommand(Guid TeamId , string? Name , string? Description) : IRequest;
