using JWT_API;
using JWT_API.Entities;
using JWT_API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApiExtensions(builder.Configuration);

builder.Services.ConfigureCors();

builder.Services.AddControllers();

builder.Services.ConfigureSwagger();

builder.Services.AddJWT(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher<User>>();
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await SeedData.InsertUserAndRolesAsync(context, passwordHasher);
    }
    catch (Exception ex)
    {
        var loggerException = loggerFactory.CreateLogger<Program>();
        loggerException.LogError(ex, "Error on migration");
    }
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
