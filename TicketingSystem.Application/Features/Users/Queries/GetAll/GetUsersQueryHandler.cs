using MediatR;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Application.Features.Users.DTOs;

namespace TicketingSystem.Application.Features.Users.Queries.GetAll;

public sealed class GetUsersQueryHandler
    (IUserRepository userRepository , ICurrentUserService currentUserService) 
    : IRequestHandler<GetUsersQuery, IReadOnlyList<UserResponse>?>
{
    public async Task<IReadOnlyList<UserResponse>?> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var tenantId = currentUserService.TenantId;
        if (tenantId is null)
            throw new UnauthorizedAccessException("Tenant context is missing.");

        var usersFromDb =await userRepository.GetListAsync(tenantId.Value, cancellationToken);
        if (usersFromDb is null)
            return [];

        return usersFromDb
            .Select(u => new UserResponse(u.Id, u.UserName!, u.Email!, u.IsActive))
            .ToList();
    }

    
}