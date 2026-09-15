using FluentValidation;

namespace TicketingSystem.Application.Features.Tenants.Commands.Create;

public sealed class CreateTenantValidation:AbstractValidator<CreateTenantCommand>
{
    public CreateTenantValidation()
    {
        RuleFor(t => t.Name)
            .NotEmpty().MaximumLength(150);

        RuleFor(t => t.Slug)
            .NotEmpty()
            .MaximumLength(100);
    }
}
