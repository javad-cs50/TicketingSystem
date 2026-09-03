using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Infrastructure.Persistence.Configurations;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TicketId)
            .IsRequired();

        builder.Property(x => x.AuthorId)
            .IsRequired();

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasOne<Ticket>()
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new
        {
            x.TicketId,
            x.CreatedAt
        });
    }
}
