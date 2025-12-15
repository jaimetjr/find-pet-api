using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities;

public class AdoptionRequest : AggregateRoot
{
    public Guid PetId { get; set; }
    public Pet Pet { get; set; }
    public string AdopterClerkId { get; set; }
    public User Adopter { get; set; }
    public string OwnerClerkId { get; set; }
    public User Owner { get; set; }
    public AdoptionRequestStatus Status { get; set; }
    public string Message { get; set; } // Required, 20-500 chars
    public string? RejectionReason { get; set; } // Optional
    public bool Active { get; set; }
    public AdoptionRequest() { }

    public AdoptionRequest(Guid petId, string adopterClerkId, string ownerClerkI, AdoptionRequestStatus status, string message)
    {
        PetId = petId;
        AdopterClerkId = adopterClerkId;
        OwnerClerkId = ownerClerkI;
        Status = status;
        Message = message;
        UpdatedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        Active = true;
    }

    public void SetStatus(AdoptionRequestStatus status, string? rejectionReason)
    {
        Status = status;
        RejectionReason = rejectionReason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetStatus(AdoptionRequestStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DeleteAdoptionRequest()
    {
        Active = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
