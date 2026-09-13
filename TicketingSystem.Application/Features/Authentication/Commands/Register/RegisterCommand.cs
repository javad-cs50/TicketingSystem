using MediatR;
using TicketingSystem.Application.Features.Authentication.DTOs;

namespace TicketingSystem.Application.Features.Authentication.Commands.Register;

public sealed record RegisterCommand(string Email , string Password ,string ConfirmPassword) : IRequest<RegisterResponse>
{
}
