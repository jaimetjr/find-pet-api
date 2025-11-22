using Domain.Enums;

namespace Domain.Entities
{
    public sealed class Pet : Entity
    {
        public string ClerkId { get; private set; }
        public string Name { get; private set; }
        public Guid TypeId { get; private set; }
        public Guid BreedId { get; private set; }
        public PetGender Gender { get;  private set; }
        public PetType Type { get; private set; }
        public PetBreed Breed { get; private set; }
        public PetSize Size { get; private set; }
        public int Age { get; private set; }
        public string Bio { get; private set; }
        public string History { get; private set; }
        public string Address { get; private set; }
        public string Neighborhood { get; private set; }
        public string CEP { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string Number { get; private set; }
        public string? Complement { get; private set; }
        public User User { get; private set; }
        public ICollection<PetImages> PetImages { get; set; }
        public ICollection<PetFavorite> PetFavorites { get; set; }

        public Pet() { }

        public void SetPet(string clerkId, string name, PetSize size, string bio, string history,
            string address, string neighborhood, string cep, string state, string city, string number, int age, PetGender gender, string? complement)
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            ClerkId = clerkId;
            Name = name;
            Size = size;
            Bio = bio;
            History = history;
            Address = address;
            Neighborhood = neighborhood;
            CEP = cep;
            State = state;
            City = city;
            Number = number;
            Age = age;
            Gender = gender;
            Complement = complement;
        }

        public void SetTypeAndBreed(Guid typeId, Guid breedId)
        {
            TypeId = typeId;
            BreedId = breedId;
        }

        public void SetPetImages(List<PetImages> petImages)
        {
            PetImages = petImages;
        }   
    }
}
