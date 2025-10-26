using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Config;
using Tasks.Infrastructure.Repositories;
using Tasks.Application.Usecases;
using Tasks.Domain.Repositories;
using System.Text.Json;
using Tasks.Security.Config.Cors;
using Tasks.Security.Config.Headers;
using Tasks.Security.Config.Permissions;

// Env configs
var envConfig = new EnvConfig();
var options = envConfig.GetDbOptions();

// Start DB + Auto Dispose();
using (var context = new AppDbContext(options))
{
    context.Database.EnsureCreated();
}

// Make builder
var builder = WebApplication.CreateBuilder(args);

// Simple Authorizatin
builder.Services.AddAuthorization(options =>
{
    foreach (var routeRole in PermissionConfig.RouteRoles)
    {
        options.AddPolicy(routeRole.Key, policy =>
        {
            policy.RequireRole(routeRole.Value);
        });
    }
});


// Controllers
builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// DI
builder.Services.AddScoped(_ => new AppDbContext(options));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<AddTask>();
builder.Services.AddScoped<GetTaskById>();

// Build app
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsPolicy();
app.ApplySecurityHeaders();
app.MapControllers();

app.Run();
