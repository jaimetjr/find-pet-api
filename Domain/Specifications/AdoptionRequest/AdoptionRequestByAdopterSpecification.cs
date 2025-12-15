using Domain.Abstractions;
using AdoptionRequestEntity = Domain.Entities.AdoptionRequest;

namespace Domain.Specifications.AdoptionRequest;

public class AdoptionRequestByAdopterSpecification : Specification<AdoptionRequestEntity>
{
    public AdoptionRequestByAdopterSpecification(string adopterClerkId)
    {
        Criteria = ar => ar.AdopterClerkId == adopterClerkId && ar.Active;
    }

    public AdoptionRequestByAdopterSpecification WithPet()
    {
        AddInclude(ar => ar.Pet);
        return this;
    }

    public AdoptionRequestByAdopterSpecification WithOwner()
    {
        AddInclude(ar => ar.Owner);
        return this;
    }

    public AdoptionRequestByAdopterSpecification WithAllIncludes()
    {
        return WithPet().WithOwner();
    }

    public AdoptionRequestByAdopterSpecification OrderedByCreatedDateDescending()
    {
        AddOrderByDescending(ar => ar.CreatedAt);
        return this;
    }
}

