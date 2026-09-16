using MediatR;
using TicketingSystem.Application.Features.Users.DTOs;

namespace TicketingSystem.Application.Features.Users.Queries.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserResponse>;