using Microsoft.AspNetCore.Identity;

namespace FilmStudioApiManagementApp.Models.AppUser
{
    public interface IAppUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool isAdmin { get; set; }
        public string Token { get; set; }
    }
}
