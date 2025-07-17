using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.UserA)
                .WithMany()
                .HasForeignKey(r => r.UserAClerkId)
                .HasPrincipalKey(x => x.ClerkId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.UserB)
                .WithMany()
                .HasForeignKey(r => r.UserBClerkId)
                .HasPrincipalKey(x => x.ClerkId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Pet)
                .WithMany()
                .HasForeignKey(x => x.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Messages)
                .WithOne(m => m.ChatRoom)
                .HasForeignKey(m => m.ChatRoomId);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.UpdatedAt)
                .IsRequired();

            // Unique index to prevent duplicate rooms for the same user pair (sorted IDs)
            builder.HasIndex(r => new { r.UserAClerkId, r.UserBClerkId }).IsUnique();
        }
    }
}
