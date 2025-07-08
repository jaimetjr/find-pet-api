using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class PetBreedConfiguration : IEntityTypeConfiguration<PetBreed>
    {
        public void Configure(EntityTypeBuilder<PetBreed> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(200);
        }
    }
}
