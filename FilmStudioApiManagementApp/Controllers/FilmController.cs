using AutoMapper;
using FilmStudioApiManagementApp.Models.Film;
using FilmStudioApiManagementApp.Models.Film.Repositories;
using FilmStudioApiManagementApp.Services.Film;
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
        private readonly IMapper mapper;

        public FilmController(IFilmRepository filmRepository, IMapper mapper)
        {
            this.filmRepository = filmRepository;
            this.mapper = mapper;
        }

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
        [HttpPut]
        public IActionResult AddFilm(Film film)
        {
            try
            {
                var addFilmAction = filmRepository.AddMovie(film);

                var filmToReturn = mapper.Map<CreateFilm>(addFilmAction);

                return Ok(filmToReturn);
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        
    }
}
