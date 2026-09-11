using MediatR;
using TicketingSystem.Application.Features.Authentication.DTOs;

namespace TicketingSystem.Application.Features.Authentication.Commands.Login;

public sealed record LoginCommand(string Email,string Password): IRequest<LoginResponse>;
