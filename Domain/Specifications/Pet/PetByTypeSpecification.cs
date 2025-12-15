using Domain.Abstractions;
using PetEntity = Domain.Entities.Pet;

namespace Domain.Specifications.Pet;

public class PetByTypeSpecification : Specification<PetEntity>
{
    public PetByTypeSpecification(Guid typeId)
    {
        Criteria = p => p.TypeId == typeId;
    }

    public PetByTypeSpecification WithBreed()
    {
        AddInclude(p => p.Breed);
        return this;
    }

    public PetByTypeSpecification WithType()
    {
        AddInclude(p => p.Type);
        return this;
    }

    public PetByTypeSpecification WithImages()
    {
        AddInclude(p => p.PetImages);
        return this;
    }
}

