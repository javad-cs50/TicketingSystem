using FluentValidation;

namespace TicketingSystem.Application.Features.Teams.Commands.Create;

public sealed class CreateTeamCommandValidator:AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(t => t.Description)
            .MaximumLength(500);
    }
}
