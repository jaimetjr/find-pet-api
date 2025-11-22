namespace Domain.Entities;

public sealed class PetFavorite : Entity
{
    public string ClerkId { get; private set; }
    public Guid PetId { get; private set; }
    public Pet Pet { get; private set; }

    public bool IsFavorite { get; private set; }

    public PetFavorite() { }

    public void SetPetFavorite(string clerkId, Guid petId, bool isFavorite)
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        ClerkId = clerkId;
        PetId = petId;
        IsFavorite = isFavorite;
    }

    public void ToggleFavorite()
    {
        IsFavorite = !IsFavorite;
        UpdatedAt = DateTime.UtcNow;
    }
}
