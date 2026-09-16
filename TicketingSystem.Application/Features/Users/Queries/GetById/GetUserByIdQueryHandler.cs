using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Users.DTOs;

namespace TicketingSystem.Application.Features.Users.Queries.GetById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository , ICurrentUserService currentUserService) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId;
        if (tenantId is null)
            throw new UnauthorizedAccessException("Tenant context is missing.");
        var userFromDb = await userRepository.GetByIdAsync(request.UserId ,tenantId!.Value  ,cancellationToken);
        if (userFromDb is null)
            throw new KeyNotFoundException($"User with id '{request.UserId}' was not found.");

        return new UserResponse( userFromDb.Id , userFromDb.UserName!, userFromDb.Email!, userFromDb.IsActive) ;
    }
}
