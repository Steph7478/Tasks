using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Config;
using Tasks.Security.Config.Cors;
using Tasks.Security.Config.Headers;
using Tasks.Security.Config.Permissions;

// env config
var envConfig = new EnvConfig();
var options = envConfig.GetDbOptions();

using (var context = new AppDbContext(options))
{
    context.Database.EnsureCreated();
}

// server builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(_ => new AppDbContext(options));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionFilter>();
});

builder.Services.AddAuthorization();

var app = builder.Build();

// configs && routes
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsPolicy();
app.ApplySecurityHeaders();
app.MapControllers();

app.Run("http://localhost:5000");
