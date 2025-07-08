namespace Domain.Entities
{
    public class PetBreed : Entity
    {
        public string Name { get; private set; }
        public Guid TypeId { get; private set; }
        public PetType Type { get; private set; }

        public PetBreed() { }

        public PetBreed(Guid id, string name, Guid typeId)
        {
            Id = id;
            Name = name;
            TypeId = typeId;
        }

        public void SetPetBreed(Guid id, string name, Guid typeId)
        {
            Id = id;
            Name = name;
            TypeId = typeId;
        }
    }
}
