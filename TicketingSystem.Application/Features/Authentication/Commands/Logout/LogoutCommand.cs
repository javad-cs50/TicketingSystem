using MediatR;

namespace TicketingSystem.Application.Features.Authentication.Commands.Logout;

public sealed record LogoutCommand(string RefreshToken):IRequest;
