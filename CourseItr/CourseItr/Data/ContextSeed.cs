using CourseItr.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CourseItr.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            

        }
    }
}
