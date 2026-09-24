using FluentValidation;

namespace TicketingSystem.Application.Features.TeamMembers.Commands.AddMember;

public sealed class AddTeamMemberCommandValidator:AbstractValidator<AddTeamMemberCommand>
{
    public AddTeamMemberCommandValidator()
    {
        RuleFor(t => t.UserId)
            .NotEmpty();

        RuleFor(t => t.TeamId)
            .NotEmpty();

    }
}
