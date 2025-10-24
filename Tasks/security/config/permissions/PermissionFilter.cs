using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Tasks.Security.Config.Permissions
{
    public class PermissionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var route = context.HttpContext.Request.Path.Value?.TrimStart('/').ToLower();
            var method = context.HttpContext.Request.Method.ToUpper();
            var user = context.HttpContext.User;

            if (route == null || !RoutePermissions.ROUTES.TryGetValue(route, out var roles))
            {
                await next();
                return;
            }

            var effectiveRoles = new List<string>();

            if (user?.Identity?.IsAuthenticated == true)
            {
                effectiveRoles.AddRange(user.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value));
            }
            else
            {
                effectiveRoles.Add("PUBLIC");
            }

            foreach (var role in effectiveRoles)
            {
                if (roles.TryGetValue(role, out var allowedMethods) && allowedMethods.Contains(method))
                {
                    await next();
                    return;
                }
            }

            context.Result = new ForbidResult();
        }
    }
}
