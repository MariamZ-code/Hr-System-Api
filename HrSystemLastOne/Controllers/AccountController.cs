using HrSystemLastOne.DTO;
using HrSystemLastOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HrSystemLastOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly UserManager<ApplicationUser> userManager;
        public readonly IConfiguration config;
        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        #region Create Account (Registeration)

       
        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterDto userDto)
        {
            if (ModelState.IsValid)
            {
                
                ApplicationUser user = new ApplicationUser()
                {
                   
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    FullName = userDto.FullName,
                    Role= userDto.Role
                    
                };
                     
                IdentityResult result=  await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Valid Account "Login" 
       
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
              ApplicationUser user= await userManager.FindByEmailAsync(userDto.Email);
                if (user!= null)
                {
                bool checkPassword = await userManager.CheckPasswordAsync(user, userDto.Password); 
                    if (checkPassword)
                    {
                        // Claims Token

                        var claims = new List<Claim>();

                        claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.FullName));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, user.Id));


                        //Get Roles
                         claims.Add(new Claim(ClaimTypes.Role, user.Role));
                      
                        //Secret Key
                        
                        SecurityKey securityKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));
                        SigningCredentials signingCred = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

                        // create Token (JSON Object)
                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"], //Url Api
                            audience: config["JWT:ValidAudience"],  // URL Angular
                            claims: claims,
                            expires: DateTime.UtcNow.AddDays(5),
                            signingCredentials: signingCred
                            );

                        // Token (String)
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration =myToken.ValidTo
                        });
                       
                    }

                }
                return Unauthorized();
            }

            return Unauthorized();
        }

  


        #endregion



    }
}
