using FilmStudioApiManagementApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FilmStudioApiManagementApp.Models.Film.Repositories
{
    public class FilmCopyRepository : IFilmCopyRepository
    {
        private readonly AppDbContext dbContext;

        public FilmCopyRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public FilmCopy AddFilmCopy(FilmCopy filmCopy)
        {
            dbContext.FilmCopies.Add(filmCopy);
            dbContext.SaveChanges();
            return filmCopy;
        }

        public IEnumerable<FilmCopy> GetAllCopies()
        {
            return dbContext.FilmCopies.OrderBy(c => c.FilmCopyId).ToList();
        }

        public FilmCopy GetFilmCopyById(int id)
        {
            return dbContext.FilmCopies.FirstOrDefault(c => c.FilmCopyId == id);
        }

        public void DeleteFilm(int id)
        {
            var getFilmCopy = GetFilmCopyById(id);
            dbContext.FilmCopies.Remove(getFilmCopy);
        }

    }
}
