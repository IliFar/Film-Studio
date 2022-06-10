using AutoMapper;
using FilmStudioApiManagementApp.Models.FilmStudio.Repositories;
using FilmStudioApiManagementApp.Services.FilmStudio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly IFilmStudioRepository filmStudioRepository;
        private readonly IMapper mapper;

        public FilmStudioController(IFilmStudioRepository filmStudioRepository, IMapper mapper)
        {
            this.filmStudioRepository = filmStudioRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var results = filmStudioRepository.GetAllFilmStudios();

                var filmStudio = mapper.Map<IEnumerable<FilmStudioService>>(results);

                return Ok(filmStudio);
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
    }
}
