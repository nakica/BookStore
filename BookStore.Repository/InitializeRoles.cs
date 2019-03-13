namespace IdentityCore.Data
{
    using BookStore.Repository;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class InitializeRoles
    {
        public static async Task Initialize(BookStoreContext context,
                                  UserManager<IdentityUser> userManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            var roles = new List<string>()
            {
                 "Administrator"
            };

            foreach(var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if(!roleExist)
                {
                    IdentityResult resultRole = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            IdentityUser user = await userManager.FindByNameAsync("npanayotov@abv.bg");

            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = "npanayotov@abv.bg"                   
                };
                await userManager.CreateAsync(user, "Parola1!");
            }
            await userManager.AddToRoleAsync(user, "Administrator");
        }
    }
}