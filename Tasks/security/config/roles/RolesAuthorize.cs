using Microsoft.AspNetCore.Authorization;
using Tasks.Security.Config.Permissions;

namespace Tasks.Security.Config.Roles;

[AttributeUsage(AttributeTargets.Method)]
public class RolesAuthorizeAttribute : AuthorizeAttribute
{
    public RolesAuthorizeAttribute(string controllerName, string actionName, string httpMethod)
    {
        if (PermissionConfig.RoutePermissions.TryGetValue($"{controllerName}.{actionName}", out var rule)
            && rule.Methods.Contains(httpMethod, StringComparer.OrdinalIgnoreCase))
        {
            Roles = string.Join(",", rule.Roles);
        }
        else
        {
            Roles = "NO_ACCESS";
        }
    }
}
