using Application.DTOs.AdoptionRequest;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("api/adoption-requests")]
[ApiController]
public class AdoptionRequestController : BaseController
{
    private readonly IAdoptionRequestService _adoptionRequestService;
    public AdoptionRequestController(IAdoptionRequestService adoptionRequestService)
    {
        _adoptionRequestService = adoptionRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateAdoptionRequestDto dto)
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _adoptionRequestService.CreateAdoptionRequest(dto, userId);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetByAdopterId()
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _adoptionRequestService.GetAdoptionRequestsByAdopterIdAsync(userId);
        return HandleResult(result);
    }

    [HttpGet("received")]
    public async Task<IActionResult> GetReceivedAdoptionRequests()
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _adoptionRequestService.GetAdoptionRequestsByOwnerIdAsync(userId);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _adoptionRequestService.GetAdoptionRequestByIdAsync(id);
        return HandleResult(result);
    }

    [HttpGet("check/{petId}")]
    public async Task<IActionResult> CheckAdoptionStatus(Guid petId)
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _adoptionRequestService.CheckAdoptionStatus(petId, userId);
        return HandleResult(result);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateAdoptionRequestDto dto)
    {
        dto.Id = id;
        var result = await _adoptionRequestService.UpdateAdoptionRequestStatus(dto);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = GetCurrentUserId();
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _adoptionRequestService.DeleteAdoptionRequest(id, userId);
        return HandleResult(result);
    }
}
