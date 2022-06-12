using FilmStudioApiManagementApp.Models.Film;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Models.FilmStudio
{
    public class FilmStudio : IdentityUser
    {
        public string City { get; set; }
        public string Role { get; set; }
        public bool isAdmin { get; set; }
        public List<FilmCopy> RentedFilmCopies { get; set; }
       
    }
}
