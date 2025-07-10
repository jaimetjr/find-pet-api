using Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected string? GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.Success)
                return Ok(result);

            return BadRequest(new ValidationProblemDetails(
                new Dictionary<string, string[]>
                {
                    { "errors", result.Errors.ToArray() }
                }));
        }

        protected IActionResult HandleResult<T>(Result<T> result, Func<Result<T>, IActionResult> successAction)
        {
            if (result.Success)
                return successAction(result);

            return BadRequest(new ValidationProblemDetails(
                new Dictionary<string, string[]>
                {
                    { "errors", result.Errors.ToArray() }
                }));
        }

        protected IActionResult NotFoundResult(string message = "Resource not found")
        {
            return NotFound(new { message });
        }
    }
} 