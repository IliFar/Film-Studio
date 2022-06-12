using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Models.Film.Repositories
{
    public interface IFilmRepository
    {
        Film AddMovie(Film film);
        IEnumerable<Film> GetAllFilms();
        Film GetFilmById(int filmId);
        bool FilmExists(int filmId);
        Film Edit(Film film);
    }
}
