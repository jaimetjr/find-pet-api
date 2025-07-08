using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Security
{
    public class SelfOrElevatedHandler : AuthorizationHandler<SelfOrElevatedRequirement, Guid>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SelfOrElevatedRequirement requirement, Guid resourceUserId)
        {
            if (context.User.IsInRole("Admin") || context.User.IsInRole("Moderator"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                if (userId == resourceUserId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
