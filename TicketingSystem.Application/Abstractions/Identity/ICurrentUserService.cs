using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Application.Abstractions.Identity;

public interface ICurrentUserService
{
    //there is no userid or tenantid for unauthenticated user 
    Guid? UserId { get; }
    Guid? TenantId { get; }

    bool IsAuthenticated { get; }

    bool IsInRole(string role);
}
