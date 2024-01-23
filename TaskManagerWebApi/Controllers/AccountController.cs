using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerWebApi.Models;

namespace TaskManagerWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest("invalid username or password");
            }

            var userEntity = await _userManager.FindByNameAsync(loginModel.UserName);

            if (userEntity == null)
            {
                return BadRequest("no user found");
            }

            // Step 2: create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", userEntity.Id.ToString()),
                new Claim("user_name", userEntity.UserName),
                new Claim("email", userEntity.Email)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                claims: claimsForToken,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);
            userEntity.SecurityToken = tokenToReturn;

            return Ok(userEntity);
        }
    }
}
