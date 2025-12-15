using Domain.Abstractions;
using AdoptionRequestEntity = Domain.Entities.AdoptionRequest;

namespace Domain.Specifications.AdoptionRequest;

public class AdoptionRequestByPetIdSpecification : Specification<AdoptionRequestEntity>
{
    public AdoptionRequestByPetIdSpecification(Guid petId)
    {
        Criteria = ar => ar.PetId == petId;
    }

    public AdoptionRequestByPetIdSpecification WithPet()
    {
        AddInclude(ar => ar.Pet);
        return this;
    }

    public AdoptionRequestByPetIdSpecification WithAdopter()
    {
        AddInclude(ar => ar.Adopter);
        return this;
    }

    public AdoptionRequestByPetIdSpecification WithOwner()
    {
        AddInclude(ar => ar.Owner);
        return this;
    }

    public AdoptionRequestByPetIdSpecification WithAllIncludes()
    {
        return WithPet().WithAdopter().WithOwner();
    }
}

