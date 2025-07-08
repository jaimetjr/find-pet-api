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
    public class PetImagesConfiguration : IEntityTypeConfiguration<PetImages>
    {
        public void Configure(EntityTypeBuilder<PetImages> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Pet)
                   .WithMany(u => u.PetImages)
                   .HasForeignKey(p => p.PetId);
        }
    }
}
