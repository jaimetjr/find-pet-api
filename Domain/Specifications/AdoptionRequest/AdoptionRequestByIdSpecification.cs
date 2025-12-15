using Domain.Abstractions;
using AdoptionRequestEntity = Domain.Entities.AdoptionRequest;

namespace Domain.Specifications.AdoptionRequest;

public class AdoptionRequestByIdSpecification : Specification<AdoptionRequestEntity>
{
    public AdoptionRequestByIdSpecification(Guid adoptionRequestId)
    {
        Criteria = ar => ar.Id == adoptionRequestId && ar.Active;
    }

    public AdoptionRequestByIdSpecification WithPet()
    {
        AddInclude(x => x.Pet);
        return this;
    }

    public AdoptionRequestByIdSpecification WithOwner()
    {
        AddInclude(ar => ar.Owner);
        return this;
    }

    public AdoptionRequestByIdSpecification WithAdopter()
    {
        AddInclude(ar => ar.Adopter);
        return this;
    }

    public AdoptionRequestByIdSpecification WithAllIncludes()
    {
        return WithPet().WithOwner().WithAdopter();
    }

    public AdoptionRequestByIdSpecification OrderedByCreatedDateDescending()
    {
        AddOrderByDescending(ar => ar.CreatedAt);
        return this;
    }
}
