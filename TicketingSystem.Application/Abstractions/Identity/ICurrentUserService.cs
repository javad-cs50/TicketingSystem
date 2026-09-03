using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Application.Abstractions.Identity;

public interface ICurrentUserService
{
    Guid UserId { get; }
    Guid? TenantId { get; }

    bool IsAuthenticated { get; }

    bool IsInRole(string role);
}
