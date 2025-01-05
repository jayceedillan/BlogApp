using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace BlogApp.Infrastructure.DbContexts
{

    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("SeedData");
            // Seed Roles
            var roles = new[] { "Admin", "Editor" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed an Admin User
            var adminUser = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                await userManager.CreateAsync(adminUser, "AdminPassword123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }


}
