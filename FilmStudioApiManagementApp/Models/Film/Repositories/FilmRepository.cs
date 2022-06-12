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
            foreach (var copy in film.FilmCopies)
            {
                dbContext.FilmCopies.Add(copy);
            }
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
            return dbContext.Films.OrderBy(f => f.Name).Include(f => f.FilmCopies).ToList();
        }

        public Film GetFilmById(int filmId)
        {
            return dbContext.Films.FirstOrDefault(f => f.FilmId == filmId);
        }

        public Film Edit(Film film)
        {
            dbContext.Films.Update(film);
            dbContext.SaveChanges();
            return film;
        }

        
    }
}
