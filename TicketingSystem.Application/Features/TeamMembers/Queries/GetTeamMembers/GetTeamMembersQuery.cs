using MediatR;
using TicketingSystem.Application.Features.TeamMembers.DTOs;

namespace TicketingSystem.Application.Features.TeamMembers.Queries.GetTeamMembers;

public sealed record GetTeamMembersQuery(Guid TeamId): IRequest<IReadOnlyList<TeamMemberResponse>>;
