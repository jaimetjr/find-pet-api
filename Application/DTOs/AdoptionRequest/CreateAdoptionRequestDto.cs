namespace Application.DTOs.AdoptionRequest;

public class CreateAdoptionRequestDto
{
    public Guid PetId { get; set; } = default!;
    public string Message { get; set; } = default!;
}
