using MediatR;

namespace TicketingSystem.Application.Features.TeamMembers.Commands.RemoveMember;

public sealed record RemoveTeamMemberCommand(Guid TeamId, Guid UserId) : IRequest;