

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FilmStudioApiManagementApp.Models.AppUser
{
    public class AppUser : IdentityUser
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public bool isAdmin { get; set; }
        public string Token { get; set; }
    }
}
