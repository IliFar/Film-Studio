using AutoMapper;
using FilmStudioApiManagementApp.Models.AppUser;
using FilmStudioApiManagementApp.Models.Film;
using FilmStudioApiManagementApp.Models.Film.Repositories;
using FilmStudioApiManagementApp.Models.FilmStudio;
using FilmStudioApiManagementApp.Services.Film;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Controllers
{

    [Route("api/films")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository filmRepository;
        private readonly IFilmCopyRepository filmCopyRepository;
        private readonly IMapper mapper;

        public FilmController(IFilmRepository filmRepository, IFilmCopyRepository filmCopyRepository, IMapper mapper)
        {
            this.filmRepository = filmRepository;
            this.filmCopyRepository = filmCopyRepository;
            this.mapper = mapper;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Authorize(Roles = UserRoles.User)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var results = filmRepository.GetAllFilms();

                var film = mapper.Map<IEnumerable<FilmService>>(results);

                return Ok(film);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [Authorize(Roles = UserRoles.User)]
        [HttpGet("{id:int}")]
        public IActionResult GetFilm(int id)
        {
            try
            {
                if (!filmRepository.FilmExists(id))
                {
                    return NotFound();
                }
                var film = filmRepository.GetFilmById(id);

                return Ok(film);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPut]
        public IActionResult AddFilm(Film film)
        {
            try
            {
                var addFilmAction = filmRepository.AddMovie(film);

                var mapFilm = mapper.Map<CreateFilm>(addFilmAction);

                var filmToReturn = mapper.Map<FilmService>(mapFilm);

                return Ok(addFilmAction);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public IActionResult UpdateFilm(Film film)
        {
            try
            {
                var result = filmRepository.Edit(film);

                return Ok(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, "Unauthorized");
            }
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Rent([FromQuery] int studioId, int filmId, [FromBody] FilmStudio filmStudio)
        {
            var film = filmRepository.GetFilmById(filmId);

            var copy = filmCopyRepository.GetFilmCopyById(film.FilmCopies[0].FilmCopyId);

            filmStudio.RentedFilmCopies.Add(copy);

            copy.StudioId = studioId;
            copy.RentedOut = true;

            return Ok(copy);
        }



    }
}
