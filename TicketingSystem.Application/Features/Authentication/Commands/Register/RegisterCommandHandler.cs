using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Features.Authentication.DTOs;

namespace TicketingSystem.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterCommand, RegisterResponse>
{
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await identityService.CreateUserAsync(request.Email,request.Password,null,cancellationToken);
        if (!result.Succeeded)
        
            throw new InvalidOperationException(string.Join(" ",result.Errors));
        return new RegisterResponse(result.UserId!.Value,request.Email);
    }
}
