using FilmStudioApiManagementApp.Models.FilmStudio;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmStudioApiManagementApp.Models.Film
{
    public class FilmCopy
    {
        [Key]
        public int FilmCopyId { get; set; }
        public int FilmId { get; set; }
        public int StudioId { get; set; }

        public bool RentedOut = false;
    }
}
