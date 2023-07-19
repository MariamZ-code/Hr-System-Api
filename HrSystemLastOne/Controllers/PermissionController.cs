using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {

        public readonly RoleManager<IdentityRole> roleManager;
        private readonly ITIContext context;

        public PermissionController(RoleManager<IdentityRole> roleManager, ITIContext context)
        {
            this.roleManager = roleManager;
            this.context = context;
        }
        [HttpPost]
        public async Task<IActionResult> ManagePermissions(PermissionFormDto permission)
        {
            var role = await roleManager.FindByIdAsync(permission.RoleId);
            if (role == null)
            {
                return NotFound();
            }
            var roleClaims = await roleManager.GetClaimsAsync(role);

            foreach (var roleClaim in roleClaims)
            {
                await roleManager.RemoveClaimAsync(role, roleClaim);
            }

            var selectedClaims = permission.RoleClaims.Where(x => x.IsSelected).ToList();

            foreach (var claim in selectedClaims)
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));
            }


            return Ok(permission);
        }
    }
}
