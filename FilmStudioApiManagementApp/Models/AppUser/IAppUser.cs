using Microsoft.AspNetCore.Identity;

namespace FilmStudioApiManagementApp.Models.AppUser
{
    public interface IAppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool isAdmin { get; set; }
    }
}
