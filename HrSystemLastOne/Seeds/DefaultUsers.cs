using HrSystemLastOne.Constants;
using HrSystemLastOne.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HrSystemLastOne.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "MaZaher",
                Email = "mz@gmail.com",
                FullName= "Mariam Zaher Fahmy",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "MaZaher23/11");
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            }
            await roleManager.SeedClaimsForAdminUser();

        }
        private static async Task SeedClaimsForAdminUser(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            await roleManager.AddPermissionClaims(adminRole, "Users");
            await roleManager.AddPermissionClaims(adminRole, "Attendance");
            await roleManager.AddPermissionClaims(adminRole, "Employee");
            await roleManager.AddPermissionClaims(adminRole, "GeneralSettings");
            await roleManager.AddPermissionClaims(adminRole, "Holidays");
            await roleManager.AddPermissionClaims(adminRole, "Home");
            await roleManager.AddPermissionClaims(adminRole, "Roles");
            await roleManager.AddPermissionClaims(adminRole, "SalaryReport");
            await roleManager.AddPermissionClaims(adminRole, "Users");

        }

        public static async Task SeedSupervisorUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "MoYasser",
                Email = "my@gmail.com",
                FullName="Mostafa Yasser",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "MoYasser23/11");
                await userManager.AddToRoleAsync(defaultUser, Roles.Supervisor.ToString());
            }

            await roleManger.SeedClaimsForSuperUser();
        }

        private static async Task SeedClaimsForSuperUser(this RoleManager<IdentityRole> roleManager)
        {
            var superRole = await roleManager.FindByNameAsync(Roles.Supervisor.ToString());
            await roleManager.AddPermissionClaims(superRole, Modules.Roles.ToString());
            await roleManager.AddPermissionClaims(superRole, Modules.Users.ToString());

            await roleManager.AddPermissionClaims(superRole, Modules.GeneralSettings.ToString());

            await roleManager.AddPermissionClaims(superRole, Modules.Holidays.ToString());

        }

        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.generatePermissionsList(module);

            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}

