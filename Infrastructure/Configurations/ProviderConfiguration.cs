using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Type)
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(p => p.ProviderKey)
                   .HasMaxLength(250);

            builder.Property(p => p.PasswordHash)
               .HasMaxLength(256);

            builder.Property(p => p.PasswordSalt)
                   .HasMaxLength(128);

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Providers)
                   .HasForeignKey(p => p.UserId);
        }
    }
}
