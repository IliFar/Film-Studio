

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FilmStudioApiManagementApp.Models.AppUser
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
    }
}
