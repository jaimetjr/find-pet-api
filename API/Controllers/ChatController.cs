using Application.DTOs.Chat;
using Application.Helpers;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllChats()
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var result = await _chatService.GetChats(userId);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPost("MarkMessageAsSeen")]
        public async Task<IActionResult> MarkMessageAsSeen([FromBody] ChatRoomIdDto room)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var result = await _chatService.MarkMessageAsSeenAsync(room.RoomId, userId);
            return HandleResult(result);
        }
    }
}
