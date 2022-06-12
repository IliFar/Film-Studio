using System.ComponentModel.DataAnnotations;

namespace FilmStudioApiManagementApp.Services.AppUser
{
    public class UserRegister : IUserRegister
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        
    }
}
