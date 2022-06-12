using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Models.Film.Repositories
{
    public interface IFilmCopyRepository
    {
        FilmCopy AddFilmCopy(FilmCopy filmCopy);
        IEnumerable<FilmCopy> GetAllCopies();
        FilmCopy GetFilmCopyById(int id);
        void DeleteFilm(int id);

    }
}
