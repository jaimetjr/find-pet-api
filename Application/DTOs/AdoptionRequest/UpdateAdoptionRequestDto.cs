using Domain.Enums;

namespace Application.DTOs.AdoptionRequest;

public class UpdateAdoptionRequestDto
{
    public Guid Id { get; set; }
    public string ClerkId { get; set; } = default!;
    public AdoptionRequestStatus Status { get; set; }
    public string? RejectionMessage { get; set; }
}
