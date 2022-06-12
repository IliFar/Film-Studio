namespace FilmStudioApiManagementApp.Services.FilmStudio
{
    public interface IRegisterFilmStudio
    {
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
