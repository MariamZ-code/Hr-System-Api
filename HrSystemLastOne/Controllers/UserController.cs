using HrSystemLastOne.Constants;
using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static HrSystemLastOne.Constants.Permissions;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITIContext context;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ITIContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ApplicationUser> users = context.Users.ToList();
            List<UserDto> userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                UserDto userDto = new UserDto
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.PasswordHash,
                    Role = user.Role,
                };
                userDtos.Add(userDto);
            }


            return Ok(userDtos);
        }


    }
}
