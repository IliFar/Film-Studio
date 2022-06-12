using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmStudioApiManagementApp.Models.Film
{
    public class Film : IFilm
    {
        [Key]
        public int FilmId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public List<FilmCopy> FilmCopies { get; set; } = new List<FilmCopy>();
        public int NumberOfCopies { get; set; } = 0;
    }
}
