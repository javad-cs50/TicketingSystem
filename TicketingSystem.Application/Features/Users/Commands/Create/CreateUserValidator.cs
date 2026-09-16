using FluentValidation;

namespace TicketingSystem.Application.Features.Users.Commands.Create;

 public sealed class CreateUserValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.UserName)
            .EmailAddress()
            .NotEmpty();

        RuleFor(u=>u.UserName)
            .NotEmpty();
    }
}
