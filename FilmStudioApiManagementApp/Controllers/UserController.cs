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
        public async Task<IActionResult> SignUp([FromBody]UserRegister userRegister)
        {

            var user = new AppUser { UserName = userRegister.Email, Email = userRegister.Email };

            var userExists = await userManager.FindByIdAsync(user.Id);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists" });
            }


            var createUser = userRepository.Create(user);

            var result = await userManager.CreateAsync(createUser, userRegister.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Failed", Message = "Failed to add user" });
            }

            // Admin Role
            if(!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                user.IsAdmin = true;
            }

            await userManager.AddToRoleAsync(user, user.Role = UserRoles.Admin);

            var userToReturn = mapper.Map<UserService>(user);
            

            return Ok(userToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]UserAuthenticate userAuthenticate)
        {
            var map = mapper.Map<AppUser>(userAuthenticate);

            var userExists = await userManager.FindByIdAsync(map.Id);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Logged in" });
            }

            var userToAuth = userRepository.Authenticate(map.Email);

            //var mapUser = mapper.Map<UserAuthenticate>(userToAuth);

            var result = await signInManager.PasswordSignInAsync(userAuthenticate.Email, userAuthenticate.Password, false, false);

            if(result == null)
            {
                return BadRequest(new Response { Status = "FAILED", Message = "Invalid Password" });
            }

            var claims = new[]
            {
                new Claim("Email", userToAuth.Email),
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

            var userToReturn = mapper.Map<AppUser>(userToAuth);

            //return Ok($"{userToReturn}{new Response { Message = tokenAsString, ExpireDate = token.ValidTo}}");
            return StatusCode(StatusCodes.Status200OK, new Response { Message = tokenAsString, ExpireDate = token.ValidTo, Status="Token Created" });

        }
    }
}
