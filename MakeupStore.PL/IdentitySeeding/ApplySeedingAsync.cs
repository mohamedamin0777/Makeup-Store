using MakeupStore.DAL.Context;
using MakeupStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace MakeupStore.PL.IdentitySeeding
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var identityConext = services.GetRequiredService<MakeupStoreDbContext>();


                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
                    await AppIdentityContextSeed.SeedRoleAsync(roleManager);

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await AppIdentityContextSeed.SeedUserAsync(userManager);

                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);

                }
            }

        }

    }


}
