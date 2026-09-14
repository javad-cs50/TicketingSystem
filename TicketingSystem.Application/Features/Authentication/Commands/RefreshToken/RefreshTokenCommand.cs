using MediatR;
using TicketingSystem.Application.Features.Authentication.DTOs;

namespace TicketingSystem.Application.Features.Authentication.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResponse>;
