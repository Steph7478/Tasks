using Tasks.Security.Config.Permissions;

namespace Tasks.Security.Config.Cors;

public static class CorsConfig
{
    private const string DefaultCorsPolicy = "DefaultCorsPolicy";

    public static void AddCorsPolicy(this IServiceCollection services, params int[] ports)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(DefaultCorsPolicy, builder =>
            {
                var origins = ports.Length > 0
                    ? ports.Select(p => $"http://localhost:{p}")
                    : ["http://localhost"];

                var allowedMethods = PermissionConfig.RoutePermissions
                    .SelectMany(p => p.Value.Methods)
                    .Distinct()
                    .ToArray();

                builder
                    .WithOrigins(origins.ToArray())
                    .WithMethods(allowedMethods)
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }

    public static void UseCorsPolicy(this WebApplication app)
        => app.UseCors(DefaultCorsPolicy);
}
