using FluentValidation;

namespace TicketingSystem.Application.Features.TeamMembers.Commands.RemoveMember;

public sealed class RemoveTeamMemberCommandValidator :AbstractValidator<RemoveTeamMemberCommand>
{
    public RemoveTeamMemberCommandValidator()
    {
        RuleFor(t => t.UserId)
            .NotEmpty();

        RuleFor(t => t.TeamId)
            .NotEmpty();
    }
}
