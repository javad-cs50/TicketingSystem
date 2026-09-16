using FluentValidation;

namespace TicketingSystem.Application.Features.Tenants.Commands.Update;

public sealed class UpdateTenantValidator:AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantValidator()
    {
        RuleFor(t => t.TenantId)
            .NotEmpty();

        RuleFor(t => t.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(t=>t.Slug)
            .NotEmpty()
            .MaximumLength(100);
    }
}
