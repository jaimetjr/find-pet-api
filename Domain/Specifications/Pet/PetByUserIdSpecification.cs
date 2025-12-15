using Domain.Abstractions;
using PetEntity = Domain.Entities.Pet;

namespace Domain.Specifications.Pet;

public class PetByUserIdSpecification : Specification<PetEntity>
{
    public PetByUserIdSpecification(string clerkId)
    {
        Criteria = p => p.ClerkId == clerkId;
    }

    public PetByUserIdSpecification WithBreed()
    {
        AddInclude(p => p.Breed);
        return this;
    }

    public PetByUserIdSpecification WithType()
    {
        AddInclude(p => p.Type);
        return this;
    }

    public PetByUserIdSpecification WithImages()
    {
        AddInclude(p => p.PetImages);
        return this;
    }

    public PetByUserIdSpecification WithFavorites()
    {
        AddInclude(p => p.PetFavorites);
        return this;
    }

    public PetByUserIdSpecification WithAllIncludes()
    {
        return WithBreed().WithType().WithImages().WithFavorites();
    }
}

