using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Models.FilmStudio.Repositories
{
    public interface IFilmStudioRepository
    {
        IEnumerable<FilmStudio> GetAllFilmStudios();
        FilmStudio GetFilmStudioById(string id);
        bool FilmStudioExists(string id);
        FilmStudio Create(FilmStudio filmStudio);
    }
}
