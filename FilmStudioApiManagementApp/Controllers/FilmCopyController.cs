using FilmStudioApiManagementApp.Models.Film;
using FilmStudioApiManagementApp.Models.Film.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmStudioApiManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmCopyController : ControllerBase
    {
        private readonly IFilmCopyRepository filmCopyRepository;

        public FilmCopyController(IFilmCopyRepository filmCopyRepository)
        {
            this.filmCopyRepository = filmCopyRepository;
        }

        [HttpPost]
        public IActionResult AddFilmCopy(FilmCopy filmCopy)
        {
            ModelState.Remove("Film");
            var result = filmCopyRepository.AddFilmCopy(filmCopy);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllFilmCopies()
        {
            var result = filmCopyRepository.GetAllCopies();
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            filmCopyRepository.DeleteFilm(id);
            return Ok();
        }
    }
}
