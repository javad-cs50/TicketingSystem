using MediatR;

namespace TicketingSystem.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommand(string UserName , string Email):IRequest<Guid>;
