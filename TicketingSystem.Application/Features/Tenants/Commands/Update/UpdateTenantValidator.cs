using FluentValidation;

namespace TicketingSystem.Application.Features.Tenants.Commands.Update;

public sealed class UpdateTenantValidator:AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantValidator()
    {
        RuleFor(t => t.TenantId)
            .NotEmpty();

        RuleFor(t => t.Name)
            .MaximumLength(150)
            .When(t=>t.Name is not null);

        RuleFor(t=>t.Slug)
            .MaximumLength(100)
            .When(t=>t.Slug is not null);

        RuleFor(t => t)
            .Must(t => t.Name is not null || t.Slug is not null);
            
    }
}
