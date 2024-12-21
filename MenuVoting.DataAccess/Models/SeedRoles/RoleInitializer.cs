using MenuVoting.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MenuVoting.DataAccess.Models.SeedRoles
{
    public class RoleInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            //var userManager = sAcope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();


            string[] roles = new string[] { "Admin", "User" };

            foreach (string role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new ApplicationRole() { Name = role });
                }
            }
        }
    }
}
