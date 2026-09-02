namespace TicketingSystem.Domain.Enums;

public enum AuditAction
{
    //Ticket
    TicketCreated = 1,
    TicketAssigned = 2,
    StatusChanged = 3,
    PriorityChanged = 4,
    CommentAdded = 5,
    TicketResolved = 6,
    TicketClosed = 7,
    TicketReopened = 8,
    //User&Identity
    UserCreated = 20,
    RoleChanged = 21,
    UserDeactivated = 22,
    //Team
    TeamCreated = 30,
    TeamMemberAdded = 31,
    TeamMemberRemoved = 32
}
