using FluentValidation;

namespace TicketingSystem.Application.Features.Users.Commands.Update;

public sealed class UpdateUserValidator:AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();

        RuleFor(u => u.UserName)
            .NotEmpty()
            .When(u => u.Email is null);

        RuleFor(u => u.Email)
            .NotEmpty()
            .When(u => u.UserName is null);

        RuleFor(u => u)
            .Must(u => u.UserName is not null || u.Email is not null);
    }
}
