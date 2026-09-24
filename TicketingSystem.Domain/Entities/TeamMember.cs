using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Domain.Exceptions;

namespace TicketingSystem.Domain.Entities;

public sealed class TeamMember
{
    public Guid TeamId { get; private set; }
    public Guid UserId { get; private set; }

    private TeamMember() { }

    public TeamMember(Guid teamId, Guid userId)
    {
        if (teamId == Guid.Empty)
            throw new DomainException("Team ID cannot be empty.");

        if (userId == Guid.Empty)
            throw new DomainException("User ID cannot be empty.");

        TeamId = teamId;
        UserId = userId;
    }
}