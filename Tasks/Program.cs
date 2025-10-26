using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Config;
using Tasks.Security.Config.Cors;
using Tasks.Security.Config.Headers;
using Tasks.Security.Config.Permissions;
using System.Text.Json;
using Tasks.Domain.Repositories;
using Tasks.Application.Usecases;
using Tasks.Infrastructure.Repositories;

// env config
var envConfig = new EnvConfig();
var options = envConfig.GetDbOptions();

// to close ORM session
using (var context = new AppDbContext(options))
{
    context.Database.EnsureCreated();
}

// server builder
var builder = WebApplication.CreateBuilder(args);

// auth
builder.Services.AddAuthorization();

// controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// scopes
builder.Services.AddScoped(_ => new AppDbContext(options));

// repo scope
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// usecases scope
builder.Services.AddScoped<AddTask>();
builder.Services.AddScoped<GetTaskById>();


// what to run in base of what was configurated before
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsPolicy();
app.ApplySecurityHeaders();
app.MapControllers();

// start server
app.Run();
