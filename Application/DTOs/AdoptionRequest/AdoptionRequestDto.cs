using Application.DTOs.Pet;
using Application.DTOs.User;
using Domain.Enums;

namespace Application.DTOs.AdoptionRequest;

public class AdoptionRequestDto
{
    public string Id { get; set; } = default!;
    public Guid PetId { get; set; } = default!;
    public PetDto Pet { get; set; } = default!;
    public string AdopterClerkId { get; set; } = default!;
    public UserDto Adopter { get; set; } = default!;
    public string OwnerClerkId { get; set; } = default!;
    public UserDto Owner { get; set; } = default!;
    public AdoptionRequestStatus Status { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string? RejectionReason { get; set; } = default!;
    public bool Active { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = default!;
}
