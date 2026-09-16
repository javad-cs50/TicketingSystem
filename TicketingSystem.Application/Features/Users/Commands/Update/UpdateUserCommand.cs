using MediatR;

namespace TicketingSystem.Application.Features.Users.Commands.Update;

public sealed record UpdateUserCommand(Guid Id , string? UserName , string? Email) :IRequest;
