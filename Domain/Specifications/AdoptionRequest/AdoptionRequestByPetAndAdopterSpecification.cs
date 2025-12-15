using Domain.Abstractions;
using AdoptionRequestEntity = Domain.Entities.AdoptionRequest;

namespace Domain.Specifications.AdoptionRequest;

public class AdoptionRequestByPetAndAdopterSpecification : Specification<AdoptionRequestEntity>
{
    public AdoptionRequestByPetAndAdopterSpecification(Guid petId, string adopterClerkId)
    {
        Criteria = ar => ar.PetId == petId && ar.AdopterClerkId == adopterClerkId && ar.Active;
    }
}

