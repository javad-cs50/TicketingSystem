using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Application.Abstractions.Identity;
using TicketingSystem.Application.Abstractions.Persistence;
using TicketingSystem.Domain.Entities;
using TicketingSystem.Infrastructure.Identity;

namespace TicketingSystem.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options), IUnitOfWork
{
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Team>()
            .HasQueryFilter(x =>
                currentUserService.TenantId == null ||
                x.TenantId == currentUserService.TenantId);

        modelBuilder.Entity<Category>()
            .HasQueryFilter(x =>
                currentUserService.TenantId == null ||
                x.TenantId == currentUserService.TenantId);

        modelBuilder.Entity<Ticket>()
            .HasQueryFilter(x =>
                currentUserService.TenantId == null ||
                x.TenantId == currentUserService.TenantId);

        modelBuilder.Entity<Notification>()
            .HasQueryFilter(x =>
                currentUserService.TenantId == null ||
                x.TenantId == currentUserService.TenantId);

        modelBuilder.Entity<AuditLog>()
            .HasQueryFilter(x =>
                currentUserService.TenantId == null ||
                x.TenantId == currentUserService.TenantId);
    }
}