using System.ComponentModel.DataAnnotations;

namespace FilmStudioApiManagementApp.Services.AppUser
{
    public interface IUserRegister
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }       
}
