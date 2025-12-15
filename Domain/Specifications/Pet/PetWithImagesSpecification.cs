using Domain.Abstractions;
using PetEntity = Domain.Entities.Pet;

namespace Domain.Specifications.Pet;

public class PetWithImagesSpecification : Specification<PetEntity>
{
    public PetWithImagesSpecification()
    {
        AddInclude(p => p.PetImages);
    }

    public PetWithImagesSpecification WithBreed()
    {
        AddInclude(p => p.Breed);
        return this;
    }

    public PetWithImagesSpecification WithType()
    {
        AddInclude(p => p.Type);
        return this;
    }

    public PetWithImagesSpecification WithFavorites()
    {
        AddInclude(p => p.PetFavorites);
        return this;
    }

    public PetWithImagesSpecification WithUser()
    {
        AddInclude(p => p.User);
        return this;
    }
}

