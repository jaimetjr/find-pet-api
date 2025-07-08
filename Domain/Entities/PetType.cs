namespace Domain.Entities
{
    public sealed class PetType : Entity
    {
        public string Name { get; private set; }

        public PetType() { }

        public PetType(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
