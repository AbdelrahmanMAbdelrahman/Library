using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Library.Infrastructure.Data;

public sealed class AppDbContextInitializer(
    AppDbContext context,
    ILogger<AppDbContextInitializer>logger,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    public async Task InitializeDatabaseAsync()
    {
        if (await context.Database.EnsureCreatedAsync())
            logger.LogError("Error Initializing database");
    }
    public async Task SeedAsync()
    {
        try {await TrySeedAsync(); }
        catch (Exception ex) {
            logger.LogError(ex.Message);
        }
    
}

    private async Task TrySeedAsync()
    {
       
        if (await roleManager.Roles.AllAsync(r => r.Name != nameof(Role.User))) {
           await roleManager.CreateAsync(new IdentityRole(nameof(Role.User)));
        }
        if (await roleManager.Roles.AllAsync(r => r.Name != nameof(Role.Librarian))) {
           await roleManager.CreateAsync(new IdentityRole(nameof(Role.Librarian)));
        }
        if (await roleManager.Roles.AllAsync(r => r.Name != nameof(Role.Admin))) {
           await roleManager.CreateAsync(new IdentityRole(nameof(Role.Admin)));
        }



       Result< AppUser> Admin = AppUser.Create("Abdelrahman","Abdelrahman$hussien10@gmail.com","01114308227", "abdelrahman_hussien@gmail.com");
        if ( userManager.Users.All(u => u.Email != Admin.Value.Email))
        {
            IdentityResult result = await userManager.CreateAsync(Admin.Value,Admin.Value.Email!);
            if (result.Succeeded)
            {
               await userManager.AddToRoleAsync(Admin.Value,nameof( Role.Admin));
            }
        }
    }
}
public static class Initializer
{
    public static async Task InitializeAsync(this WebApplication app )
    {
        AsyncServiceScope scope = app.Services.CreateAsyncScope();
        AppDbContextInitializer initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
        await initializer.InitializeDatabaseAsync();
        await initializer.SeedAsync();
    }
}