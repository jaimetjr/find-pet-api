using Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.ChatRoom)
                .WithMany(r => r.Messages)
                .HasForeignKey(m => m.ChatRoomId);

            builder.HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .HasPrincipalKey(x => x.ClerkId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(m => m.Content)
                .IsRequired()
                .HasMaxLength(2000); // Adjust as needed

            builder.Property(m => m.WasSeen)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(m => m.SeenByClerkId)
                .HasMaxLength(100);

            builder.Property(m => m.WasDelivered)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(m => m.Recipient)
               .WithMany()
               .HasForeignKey(m => m.RecipientId)
               .HasPrincipalKey(x => x.ClerkId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(m => m.SentAt)
                .IsRequired();
        }
    }
}
