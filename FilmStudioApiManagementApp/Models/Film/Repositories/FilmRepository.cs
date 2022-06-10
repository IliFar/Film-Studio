using FilmStudioApiManagementApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FilmStudioApiManagementApp.Models.Film.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly AppDbContext dbContext;

        public FilmRepository (AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Film AddMovie(Film film)
        {
            dbContext.Films.Add(film);
            dbContext.SaveChanges();
            return film;
        }

        public bool FilmExists(int filmId)
        {
            return dbContext.Films.Any(f => f.FilmId == filmId);
        }

        public IEnumerable<Film> GetAllFilms()
        {
            return dbContext.Films.OrderBy(f => f.Name).ToList();
        }

        public Film GetFilmById(int filmId)
        {
            return dbContext.Films.FirstOrDefault(f => f.FilmId == filmId);
        }

        public int GetFilmCopies(int filmId)
        {
            var filmCopy = dbContext.FilmCopies.Where(f => f.Film.FilmId == filmId);

            if (filmCopy.Count() <= 0)
            {
                return 0;
            }

            return filmCopy.Count();
        }
    }
}
