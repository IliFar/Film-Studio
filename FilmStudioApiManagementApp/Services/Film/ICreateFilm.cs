using System;

namespace FilmStudioApiManagementApp.Services.Film
{
    public interface ICreateFilm
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
    }
}
