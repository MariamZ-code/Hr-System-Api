using HrSystemLastOne.Constants;
using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        public readonly RoleManager<IdentityRole> roleManager;
        private readonly ITIContext context;
      


        public RoleController(RoleManager<IdentityRole> roleManager,ITIContext context)
        {
            this.roleManager = roleManager;
            this.context = context;
       

        }

        #region GetAll Roles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return Ok(roles);
        }
        #endregion

        #region Add New Roles
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleFormDto roleFormDto)
        {
            if (!ModelState.IsValid)
            {
                var roles = await roleManager.Roles.ToListAsync();
                return Ok(roles);
            }
            var roleExist = await roleManager.RoleExistsAsync(roleFormDto.Name);
            if (roleExist)
            {
                ModelState.AddModelError("Name", "Role Is Exist");
                return Content("Role Is Exist");
            }
            var newRole = await roleManager.CreateAsync(new IdentityRole(roleFormDto.Name.Trim()));
            return Ok(newRole);
        }

        #endregion

        [HttpGet("{roleId}")]
        public async Task<IActionResult> ManagePermissions(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }
            var roleClaims = roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            var allClaims = Permissions.generateAllPemissions();
            var allper = context.RoleClaims.ToList();
            int i = 0;
            var allPermissions = allClaims.Select(x => new CheckBoxDto { DisplayValue = x }).ToList();
            foreach (var permission in allPermissions)
            {
                permission.DisplayID = allper[i].Id.ToString();
                if (roleClaims.Any(x => x == permission.DisplayValue))
                {
                    permission.IsSelected = true;
                }
                i++;
            }
            var permissionRole = new PermissionFormDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                RoleClaims = allPermissions

            };
            //await roleManager.AddClaimAsync(role, new Claim("Can add roles", "add.role"));

            return Ok(permissionRole);
        }


        //[HttpPost("{id}")]
        //public async Task<IActionResult> ManagePermissions(PermissionFormDto permission)
        //{
        //    var role = await roleManager.FindByIdAsync(permission.RoleId);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    var roleClaims = await roleManager.GetClaimsAsync(role);

        //    foreach (var roleClaim in roleClaims)
        //    {
        //        await roleManager.RemoveClaimAsync(role, roleClaim);
        //    }

        //    var selectedClaims = permission.RoleClaims.Where(x => x.IsSelected).ToList();

        //    foreach (var claim in selectedClaims)
        //    {
        //        await roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));
        //    }


        //    return Ok(permission);
        //}
    



    }
}
