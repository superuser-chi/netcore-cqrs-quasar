using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Persistence.Identity;
using Microsoft.Extensions.Logging;

namespace Persistence
{
    public class SeedData<T>
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider, string password)
        {
            var services = new ServiceCollection();
            // Make sure db context is added.
            services.AddDbContext<ApplicationDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Make sure migrations are applied
                scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                var logger = scope.ServiceProvider.GetService<ILogger<T>>();
                context.Database.Migrate();

                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                EnsureRoles(roleManager, logger).Wait();

                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                EnsureUsers(userManager, context, logger, password).Wait();
            }
        }
        private static async Task EnsureRoles(RoleManager<IdentityRole> roleManager, ILogger<T> logger)
        {
            if (!roleManager.Roles.Any())
            {
                logger.LogInformation("Roles being populated");
                foreach (var role in getRoles())
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            else
            {
                logger.LogWarning("Roles already populated");
            }
        }
        private static async Task EnsureUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<T> logger, string password)
        {

            if (!userManager.Users.Any())
            {
                logger.LogInformation("Users being populated");
                foreach (var user in getUsers(context))
                {
                    user.UserName = user.UserName;
                    try
                    {
                        IdentityResult identityResult = await userManager.CreateAsync(user, password);
                        if (identityResult.Succeeded)
                        {
                            var newUser = await userManager.FindByNameAsync(user.UserName);
                            await userManager.AddToRoleAsync(newUser, user.Role);
                        }

                    }
                    catch (Exception e)
                    {
                        logger.LogError($"Could not create user {user.UserName} because {e.Message}");
                        throw;
                    }
                }
            }
            else
            {
                logger.LogWarning("Users already populated");
            }
        }

        #region  SeedUser
        private static ApplicationUser[] getUsers(ApplicationDbContext context)
        {
            return new[] {
                new ApplicationUser
                {
                    UserName = "giftm@centralbank.org.sz",
                    FullName = "Mavuso Gift N.",
                    PhoneNumber = "76123456",
                    Email = "giftm@centralbank.org.sz",
                    Role = UserRoles.Admin,
                    EmailConfirmed = true
                },
            };
        }
        #endregion
        #region SeedRoles
        public static string[] getRoles()
        {
            return new string[] { UserRoles.Admin, UserRoles.User };
        }
        #endregion
    }
}