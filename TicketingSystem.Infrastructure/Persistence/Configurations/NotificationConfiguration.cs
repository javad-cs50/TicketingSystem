using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Configurations
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
           
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.TenantId)
           .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.IsRead)
                .IsRequired();

            builder.Property(x => x.ReadAt);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt);

            builder.HasIndex(x => new
            {
                x.TenantId,
                x.UserId,
                x.IsRead
            });

            builder.HasIndex(x => new
            {
                x.UserId,
                x.CreatedAt
            });
        }
    }
    }

