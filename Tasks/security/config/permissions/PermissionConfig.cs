namespace Tasks.Security.Config.Permissions
{
    public static class PermissionConfig
    {
        public static readonly Dictionary<string, string[]> RouteRoles = new()
        {
            // { $"{nameof(TasksController)}.{nameof(TasksController.CreateTask)}", new[] { "ADMIN" } },
            // { $"{nameof(TasksController)}.{nameof(TasksController.GetTaskById)}", new[] { "ADMIN", "USER" } },
        };
    }

}
