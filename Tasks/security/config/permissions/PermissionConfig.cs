using Tasks.Presentation.Controllers;

namespace Tasks.Security.Config.Permissions;

public static class PermissionConfig
{
    public static readonly Dictionary<string, PermissionRule> RoutePermissions = new()
    {
        // [Key<TasksController>(c => c.CreateTask)] = new(["ADMIN"], ["POST"]),
        // [Key<TasksController>(c => c.GetTaskById)] = new(["ADMIN", "USER"], ["GET"]),
        // [Key<TasksController>(c => c.UpdateTask)] = new(["ADMIN"], ["PUT"]),
        // [Key<TasksController>(c => c.DeleteTask)] = new(["ADMIN"], ["DELETE"])
    };

    private static string Key<T>(System.Linq.Expressions.Expression<Func<T, Delegate>> selector)
        => $"{typeof(T).Name}.{((System.Linq.Expressions.MemberExpression)selector.Body).Member.Name}";
}

public record PermissionRule(string[] Roles, string[] Methods);
