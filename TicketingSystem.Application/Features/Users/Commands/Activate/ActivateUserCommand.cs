using MediatR;

namespace TicketingSystem.Application.Features.Users.Commands.Activate;

public sealed record ActivateUserCommand(Guid Id) : IRequest;
