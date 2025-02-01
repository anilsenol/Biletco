using biletcoo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace biletcoo.Data
{
    public static class ContextSeed
    {

        public static async Task SeedRolesAsync(UserManager<users> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Organizer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
        }
    }
}
