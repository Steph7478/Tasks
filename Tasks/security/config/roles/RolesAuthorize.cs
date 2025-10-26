using Microsoft.AspNetCore.Authorization;
using Tasks.Security.Config.Permissions;

namespace Tasks.Security.Config.Roles
{
    [AttributeUsage(AttributeTargets.Method)]

    public class RolesAuthorizeAttribute : AuthorizeAttribute
    {
        public RolesAuthorizeAttribute(string controllerName, string actionName)
        {
            if (PermissionConfig.RouteRoles.TryGetValue($"{controllerName}.{actionName}", out var roles) && roles.Length > 0)
            {
                Roles = string.Join(",", roles);
            }
            else
            {
                Roles = "NO_ACCESS";
            }
        }


    }
}
