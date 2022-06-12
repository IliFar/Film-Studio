namespace FilmStudioApiManagementApp.Services.FilmStudio
{
    public class RegisterFilmStudio : IRegisterFilmStudio
    {
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
