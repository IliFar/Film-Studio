using FilmStudioApiManagementApp.Models.FilmStudio;

namespace FilmStudioApiManagementApp.Models.Film
{
    public class FilmCopy
    {
        public string FilmCopyId { get; set; }
        public Film Film { get; set; }
        public bool RentedOut { get; set; } = false;
    }
}
