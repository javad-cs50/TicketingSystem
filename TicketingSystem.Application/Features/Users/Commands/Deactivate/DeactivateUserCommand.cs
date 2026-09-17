using MediatR;

namespace TicketingSystem.Application.Features.Users.Commands.Deactivate;

public sealed record DeactivateUserCommand(Guid Id) : IRequest;