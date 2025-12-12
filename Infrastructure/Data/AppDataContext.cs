using Domain.Entities;
using Domain.Entities.Chat;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDataContext(DbContextOptions<AppDataContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<Pet> Pets => Set<Pet>();
        public DbSet<PetImages> PetImages => Set<PetImages>();
        public DbSet<PetBreed> PetBreeds => Set<PetBreed>();
        public DbSet<PetType> PetTypes => Set<PetType>();
        public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
        public DbSet<PetFavorite> PetFavorites => Set<PetFavorite>();
        public DbSet<AdoptionRequest> AdoptionRequests => Set<AdoptionRequest>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
            modelBuilder.ApplyConfiguration(new ChatRoomConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            //modelBuilder.ApplyConfiguration(new PetConfiguration());
            //modelBuilder.ApplyConfiguration(new PetImagesConfiguration());
            //modelBuilder.ApplyConfiguration(new PetBreedConfiguration());
            //modelBuilder.ApplyConfiguration(new PetTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

