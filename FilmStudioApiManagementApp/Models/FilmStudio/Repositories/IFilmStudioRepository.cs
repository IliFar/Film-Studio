using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Models.FilmStudio.Repositories
{
    public interface IFilmStudioRepository
    {
        IEnumerable<FilmStudio> GetAllFilmStudios();
        FilmStudio GetFilmStudioById(int id);
        bool FilmStudioExists(int id);
    }
}
