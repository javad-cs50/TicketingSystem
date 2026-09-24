using MediatR;

namespace TicketingSystem.Application.Features.TeamMembers.Commands.AddMember;

public sealed record AddTeamMemberCommand(Guid TeamId , Guid UserId) : IRequest;
