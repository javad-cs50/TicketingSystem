using MediatR;
using TicketingSystem.Application.Features.Users.DTOs;

namespace TicketingSystem.Application.Features.Users.Queries.GetAll;

public sealed record GetUsersQuery : IRequest<IReadOnlyList< UserResponse>?>;
