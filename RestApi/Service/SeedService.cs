using RestApi.Data;
using Microsoft.AspNetCore.Identity;
using RestApi.Models;

namespace RestApi.Services
{
    public class SeedService
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                logger.LogInformation("Starting database seeding...");
                await context.Database.EnsureCreatedAsync();

                // Seed roles
                logger.LogInformation("Seeding roles...");
                await AddRoleAsync(rolemanager, "Admin");
                await AddRoleAsync(rolemanager, "User");

                // Seed admin user
                logger.LogInformation("Seeding admin user...");
                var adminEmail = "admin@example.com";
                if (await usermanager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new Users
                    {
                        FullName = "admin",
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await usermanager.CreateAsync(adminUser, "Admin123!");
                    if (result.Succeeded)
                    {
                        await usermanager.AddToRoleAsync(adminUser, "Admin");
                        logger.LogInformation("Admin user created successfully.");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}'.");
                }
            }
        }
    }
}
