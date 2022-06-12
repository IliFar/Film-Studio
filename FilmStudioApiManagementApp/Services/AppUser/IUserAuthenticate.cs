namespace FilmStudioApiManagementApp.Services.AppUser
{
    public interface IUserAuthenticate
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
