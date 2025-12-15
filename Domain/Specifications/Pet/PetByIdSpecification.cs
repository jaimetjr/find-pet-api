using Domain.Abstractions;
using PetEntity = Domain.Entities.Pet;

namespace Domain.Specifications.Pet;

public class PetByIdSpecification : Specification<PetEntity>
{
    public PetByIdSpecification(Guid id)
    {
        Criteria = p => p.Id == id;
    }

    public PetByIdSpecification WithBreed()
    {
        AddInclude(p => p.Breed);
        return this;
    }

    public PetByIdSpecification WithType()
    {
        AddInclude(p => p.Type);
        return this;
    }

    public PetByIdSpecification WithImages()
    {
        AddInclude(p => p.PetImages);
        return this;
    }

    public PetByIdSpecification WithUser()
    {
        AddInclude(p => p.User);
        return this;
    }

    public PetByIdSpecification WithFavorites()
    {
        AddInclude(p => p.PetFavorites);
        return this;
    }

    public PetByIdSpecification WithAllIncludes()
    {
        return WithBreed().WithType().WithImages().WithUser().WithFavorites();
    }
}

