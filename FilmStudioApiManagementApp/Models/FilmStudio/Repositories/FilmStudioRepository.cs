using FilmStudioApiManagementApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace FilmStudioApiManagementApp.Models.FilmStudio.Repositories
{
    public class FilmStudioRepository : IFilmStudioRepository
    {
        private readonly AppDbContext dbContext;

        public FilmStudioRepository (AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool FilmStudioExists(string id)
        {
            return dbContext.FilmStudios.Any(s => s.Id == id);
        }

        public IEnumerable<FilmStudio> GetAllFilmStudios()
        {
            return dbContext.FilmStudios.OrderBy(s => s.UserName).ToList();
        }

        public FilmStudio GetFilmStudioById(string id)
        {
            return dbContext.FilmStudios.FirstOrDefault(s => s.Id == id);
        }

        public FilmStudio Create(FilmStudio filmStudio)
        {
            dbContext.FilmStudios.Add(filmStudio);
            return filmStudio;
        }

    }
}
