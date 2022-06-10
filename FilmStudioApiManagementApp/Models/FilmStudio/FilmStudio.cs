using FilmStudioApiManagementApp.Models.Film;
using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Models.FilmStudio
{
    public class FilmStudio : IFilmStudio
    {
        public int FilmStudioId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
       
    }
}
