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
    public class PetTypeConfiguration : IEntityTypeConfiguration<PetType>
    {
        public void Configure(EntityTypeBuilder<PetType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        }
    }
}
