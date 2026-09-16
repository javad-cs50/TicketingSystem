using MediatR;
using TicketingSystem.Application.Abstractions.Persistence;

namespace TicketingSystem.Application.Features.Users.Commands.Update;

public sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand>
{
    public Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        
    }
}
