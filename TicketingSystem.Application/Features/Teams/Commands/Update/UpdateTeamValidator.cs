using FluentValidation;
using TicketingSystem.Application.Features.Teams.Commands.Create;

namespace TicketingSystem.Application.Features.Teams.Commands.Update;

public sealed class UpdateTeamValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamValidator()
    {
        RuleFor(t => t.TeamId)
            .NotEmpty();

        RuleFor(t => t.Name)
            .MaximumLength(100)
            .When(t=>t.Description is not null);

        RuleFor(t => t.Description)
            .MaximumLength(500)
            .When(t => t.Name is not null);

        RuleFor(t=>t)
            .Must(t => t.Description is not null ||t.Name is not null);
    }
}
