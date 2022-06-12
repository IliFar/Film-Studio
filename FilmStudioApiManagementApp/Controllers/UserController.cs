using Microsoft.Extensions.Configuration;
using AutoMapper;
using FilmStudioApiManagementApp.Models.AppUser;
using FilmStudioApiManagementApp.Models.AppUser.Repositories;
using FilmStudioApiManagementApp.Services.AppUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace FilmStudioApiManagementApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IUserRepository userRepository, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.roleManager = roleManager;
            this.config = config;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]AppUser appUser)
        {
            var userExists = await userManager.FindByNameAsync(appUser.Id);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists" });
            }

            var addUserAction = userRepository.Create(appUser);

            var mapAppUser = mapper.Map<UserRegister>(addUserAction);

            var result = await userManager.CreateAsync(addUserAction, mapAppUser.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Failed", Message = "Failed to add user" });
            }

            // Admin Role
            if(!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                appUser.IsAdmin = true;
            }

            await userManager.AddToRoleAsync(appUser, appUser.Role = UserRoles.Admin);

            var userToReturn = mapper.Map<UserService>(addUserAction);
            

            return Ok(userToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]AppUser appUser)
        {
            var userExists = await userManager.FindByNameAsync(appUser.Id);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Logged in" });
            }

            var userToAuth = userRepository.Authenticate(appUser.UserName, appUser.Password);
            var mapUser = mapper.Map<UserAuthenticate>(userToAuth);
            var result = await userManager.CheckPasswordAsync(userToAuth, mapUser.Password);

            if(!result)
            {
                return BadRequest(new Response { Status = "FAILED", Message = "Invalid Password" });
            }

            var claims = new[]
            {
                new Claim("Email", mapUser.Username),
                new Claim(ClaimTypes.NameIdentifier, userToAuth.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: config["JWT:ValidIssuer"],
                audience: config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            var userToReturn = mapper.Map<UserService>(mapUser);

            return Ok($"{userToReturn}{new Response { Message = tokenAsString, ExpireDate = token.ValidTo}}");

        }
    }
}
