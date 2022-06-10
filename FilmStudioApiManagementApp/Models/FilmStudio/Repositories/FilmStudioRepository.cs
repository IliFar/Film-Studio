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

        public bool FilmStudioExists(int id)
        {
            return dbContext.FilmStudios.Any(s => s.FilmStudioId == id);
        }

        public IEnumerable<FilmStudio> GetAllFilmStudios()
        {
            return dbContext.FilmStudios.OrderBy(s => s.Name).ToList();
        }

        public FilmStudio GetFilmStudioById(int id)
        {
            return dbContext.FilmStudios.FirstOrDefault(s => s.FilmStudioId == id);
        }
    }
}
