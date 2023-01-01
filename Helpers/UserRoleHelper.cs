using Coffee_Project.Data;
using Microsoft.AspNetCore.Identity;

namespace Coffee_Project.Helpers
{
    public static class UserRoleHelper
    {
        public static  async Task CreateRoles(WebApplication app)
        {
            //adding customs roles : Question 1
            var RoleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = app.Services.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 2
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here you could create a super user who will maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
            };

            string userPWD = "Abc@1230";
            var _user = await UserManager.FindByEmailAsync("admin@admin.com");

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role : Question 3
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
    }
}
