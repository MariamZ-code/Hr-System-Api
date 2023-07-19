using HrSystemLastOne.Constants;
using Microsoft.AspNetCore.Identity;

namespace HrSystemLastOne.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManger)
        {
            if (!roleManger.Roles.Any())
            {
                await roleManger.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManger.CreateAsync(new IdentityRole(Roles.Supervisor.ToString()));
                await roleManger.CreateAsync(new IdentityRole(Roles.TeamLeader.ToString()));
                await roleManger.CreateAsync(new IdentityRole(Roles.Hr.ToString()));

            }
        }
    }
}
