namespace Tasks.Security.Config.Permissions
{
    public static class RoutePermissions
    {
        public static readonly Dictionary<string, Dictionary<string, string[]>> ROUTES = new()
        {
            ["tasks"] = new() { ["PUBLIC"] = ["GET", "PUT", "DELETE", "POST"], },
        };
    }
}
