using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Configurations;

public sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId)
            .IsRequired();

        builder.Property(x => x.TicketNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(10_000);

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.TeamId);

        builder.Property(x => x.AgentId);

        builder.Property(x => x.CategoryId);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Priority)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.ResolvedAt);

        builder.Property(x => x.ClosedAt);

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.TicketNumber
        })
        .IsUnique();

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.Status
        });

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.Priority
        });

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.AgentId
        });

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.TeamId
        });

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.CustomerId
        });

        builder.HasIndex(x => new
        {
            x.TenantId,
            x.CreatedAt
        });
    }
}
