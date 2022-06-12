using AutoMapper;
using FilmStudioApiManagementApp.Models.AppUser;
using FilmStudioApiManagementApp.Models.AppUser.Repositories;
using FilmStudioApiManagementApp.Models.FilmStudio;
using FilmStudioApiManagementApp.Models.FilmStudio.Repositories;
using FilmStudioApiManagementApp.Services.AppUser;
using FilmStudioApiManagementApp.Services.FilmStudio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmStudioApiManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly IFilmStudioRepository filmStudioRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly RoleManager<IdentityRole> roleManager;

        public FilmStudioController(IFilmStudioRepository filmStudioRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IUserRepository userRepository, RoleManager<IdentityRole> roleManager)
        {
            this.filmStudioRepository = filmStudioRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var results = filmStudioRepository.GetAllFilmStudios();

                if (User.IsInRole("user") || !User.Identity.IsAuthenticated)
                {
                    var filmStudio = mapper.Map<IEnumerable<FilmStudioService>>(results);

                    return Ok(filmStudio);
                }

                return Ok(results);
                

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        [HttpGet("{id:int}")]
        public IActionResult GetFilmStudio(string id)
        {
            try
            {
                if (!filmStudioRepository.FilmStudioExists(id))
                {
                    return NotFound();
                }
                var filmStudio = filmStudioRepository.GetFilmStudioById(id);

                return Ok(filmStudio);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterFilmStudio registerFilmStudio)
        {
            var user = new FilmStudio { UserName = registerFilmStudio.Email, Email = registerFilmStudio.Email };

            var userExists = await userManager.FindByIdAsync(user.Id);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists" });
            }


            var createUser = filmStudioRepository.Create(user);

            var map = mapper.Map<AppUser>(createUser);

            var result = await userManager.CreateAsync(map, registerFilmStudio.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Failed", Message = "Failed to add user" });
            }

            // User Role
            
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            map.IsAdmin = false;
            

            await userManager.AddToRoleAsync(map, user.Role = UserRoles.Admin);

            var userToReturn = mapper.Map<FilmStudioService>(user);


            return Ok(userToReturn);
            //var userExists = await userManager.FindByNameAsync(appUser.Id);

            //if (userExists != null)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists" });
            //}

            //var addUserAction = userRepository.Create(appUser);

            //var mapAppUser = mapper.Map<RegisterFilmStudio>(addUserAction);

            //var result = await userManager.CreateAsync(addUserAction, mapAppUser.Password);

            //if (!result.Succeeded)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Failed", Message = "Failed to add user" });
            //}

            //// User Role
            //await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //await userManager.AddToRoleAsync(appUser, appUser.Role = UserRoles.User);

            //var userToReturn = mapper.Map<FilmStudioService>(addUserAction);

            //return Ok(userToReturn);
        }
    }
}
