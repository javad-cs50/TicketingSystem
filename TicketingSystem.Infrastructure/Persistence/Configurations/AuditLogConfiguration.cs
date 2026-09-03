using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Configurations
{
    public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Action)
                .IsRequired();

            builder.Property(x => x.EntityType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.EntityId)
                .IsRequired();

            builder.Property(x => x.Data);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt);

            builder.HasIndex(x => new
            {
                x.TenantId,
                x.EntityType,
                x.EntityId
            });

            builder.HasIndex(x => new
            {
                x.TenantId,
                x.CreatedAt
            });
        }
    }
}
