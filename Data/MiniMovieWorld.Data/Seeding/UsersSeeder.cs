namespace MovieDb.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MiniMovieWorld.Data;
    using MiniMovieWorld.Data.Models;
    using MiniMovieWorld.Data.Seeding;

    public class UsersSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersSeeder(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var adminRole = new ApplicationRole
            {
                Name = "Admin",
            };

            await this.roleManager.CreateAsync(adminRole);

            var applicationUser = new ApplicationUser
            {
                UserName = "stennlyy",
                Email = "stenli.bg@mail.bg",
                EmailConfirmed = true,
            };

            var password = "Akmani12";

            var checkUser = await this.userManager.CreateAsync(applicationUser, password);

            if (checkUser.Succeeded)
            {
                await this.userManager.AddToRoleAsync(applicationUser, "Admin");
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
