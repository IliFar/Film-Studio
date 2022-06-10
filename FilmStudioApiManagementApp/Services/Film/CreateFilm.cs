using FilmStudioApiManagementApp.Models.Film;
using System;
using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Services.Film
{
    public class CreateFilm : ICreateFilm
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
    }
}
