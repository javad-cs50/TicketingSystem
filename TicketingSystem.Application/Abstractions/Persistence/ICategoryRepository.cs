using System;
using System.Collections.Generic;
using System.Text;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Application.Abstractions.Persistence;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(Category category, CancellationToken cancellationToken);
}
