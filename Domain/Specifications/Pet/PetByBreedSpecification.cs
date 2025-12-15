using Domain.Abstractions;
using PetEntity = Domain.Entities.Pet;

namespace Domain.Specifications.Pet;

public class PetByBreedSpecification : Specification<PetEntity>
{
    public PetByBreedSpecification(Guid breedId)
    {
        Criteria = p => p.BreedId == breedId;
    }

    public PetByBreedSpecification WithBreed()
    {
        AddInclude(p => p.Breed);
        return this;
    }

    public PetByBreedSpecification WithType()
    {
        AddInclude(p => p.Type);
        return this;
    }

    public PetByBreedSpecification WithImages()
    {
        AddInclude(p => p.PetImages);
        return this;
    }
}

