using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Avatar).HasMaxLength(1000);
            builder.Property(u => u.Phone).HasMaxLength(30);
            builder.Property(u => u.Address).HasMaxLength(100);
            builder.Property(u => u.CEP).HasMaxLength(10);
            builder.Property(u => u.City).HasMaxLength(100);
            builder.Property(u => u.State).HasMaxLength(100);
            builder.Property(u => u.Neighborhood).HasMaxLength(100);
            builder.Property(u => u.Bio).HasMaxLength(500);
            builder.Property(u => u.Number).HasMaxLength(10);
            builder.Property(u => u.Complement).HasMaxLength(50);
            builder.Property(u => u.Notifications).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.UpdatedAt).IsRequired();
            builder.Property(u => u.ClerkId).IsRequired();
            builder.HasIndex(u => u.ClerkId).IsUnique();
            builder.Property(u => u.ClerkId).HasMaxLength(100);
            builder.Property(u => u.ApprovalStatus)
                .HasConversion<int>()
                .IsRequired();
            builder.Property(u => u.Role)
                   .HasConversion<int>()
                   .IsRequired();

            builder.HasMany(u => u.Providers)
                   .WithOne(p => p.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Pets)
                   .WithOne(p => p.User)
                   .HasForeignKey(x => x.UserId)
                   .HasPrincipalKey(x => x.ClerkId)
                     .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
