using AutoMapper;
using FilmStudioApiManagementApp.Models.AppUser;
using FilmStudioApiManagementApp.Models.AppUser.Repositories;
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
    [Route("api/[controller]/[action]")]
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
        public IActionResult GetFilmStudio(int id)
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
        public async Task<IActionResult> Register([FromBody] AppUser appUser)
        {
            var userExists = await userManager.FindByNameAsync(appUser.Id);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User Already Exists" });
            }

            var addUserAction = userRepository.Create(appUser);

            var mapAppUser = mapper.Map<RegisterFilmStudio>(addUserAction);

            var result = await userManager.CreateAsync(addUserAction, mapAppUser.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Failed", Message = "Failed to add user" });
            }

            // User Role
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            await userManager.AddToRoleAsync(appUser, appUser.Role = UserRoles.User);

            var userToReturn = mapper.Map<FilmStudioService>(addUserAction);

            return Ok(userToReturn);
        }
    }
}
