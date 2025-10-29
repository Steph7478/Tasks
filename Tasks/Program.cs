using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Config;
using Tasks.Infrastructure.Repositories;
using Tasks.Application.Usecases;
using Tasks.Domain.Repositories;
using System.Text.Json;
using Tasks.Security.Config.Cors;
using Tasks.Security.Config.Headers;
using Tasks.Security.Config.Permissions;
using Tasks.Domain.Services;

// Env config
var envConfig = new EnvConfig();
var options = envConfig.GetDbOptions();

// DB local
using (var context = new AppDbContext(options))
{
    context.Database.EnsureCreated();
}

// Builder
var builder = WebApplication.CreateBuilder(args);

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    foreach (var (routeKey, rule) in PermissionConfig.RoutePermissions)
    {
        options.AddPolicy(routeKey, policy =>
        {
            policy.RequireRole(rule.Roles);
        });
    }
});

// Controllers + JSON config
builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// DI - DbContext + GetDbOptions from EnvConfig
builder.Services.AddScoped(_ => new AppDbContext(options));

// DI - Repo && UseCases
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<TaskDomainService>();
builder.Services.AddScoped<GetTaskById>();
builder.Services.AddScoped<GetAllTasksUseCase>();
builder.Services.AddScoped<AddTask>();
builder.Services.AddScoped<UpdateTaskUseCase>();
builder.Services.AddScoped<UpdateStatusUseCase>();
builder.Services.AddScoped<DeleteTaskUseCase>();

// Build app
var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseAuthorization();
app.ApplySecurityHeaders();

// Map controllers
app.MapControllers();

// Run
app.Run();

//dotnet run --launch-profile "https"