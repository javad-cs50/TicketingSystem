using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Abstractions.Persistence;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Ticket ticket, CancellationToken cancellationToken);
}
