using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pet");
            builder.HasKey(p => p.Id);

            builder.Property(u => u.TypeId).IsRequired();
            builder.Property(u => u.PetImages).IsRequired();
            builder.Property(u => u.BreedId).IsRequired();
            builder.Property(u => u.Bio).IsRequired().HasMaxLength(500);
            builder.Property(u => u.History).IsRequired().HasMaxLength(500);

            builder.Property(u => u.Address).IsRequired().HasMaxLength(200);
            builder.Property(u => u.CEP).IsRequired().HasMaxLength(10);
            builder.Property(u => u.City).IsRequired().HasMaxLength(100);
            builder.Property(u => u.State).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Neighborhood).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Bio).IsRequired().HasMaxLength(500);
            builder.Property(u => u.Number).IsRequired().HasMaxLength(10);
            builder.Property(u => u.Complement).HasMaxLength(50);
            builder.Property(u => u.Age).IsRequired().HasDefaultValue(0);

            builder.HasOne(p => p.Type)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Breed)
                   .WithMany()
                   .HasForeignKey(p => p.BreedId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
