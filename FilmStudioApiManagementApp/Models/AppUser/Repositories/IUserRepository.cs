using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Models.AppUser.Repositories
{
    public interface IUserRepository
    {
        AppUser Authenticate(string username);
        IEnumerable<AppUser> GetAll();
        AppUser GetById(int id);
        AppUser Create(AppUser user);
        bool userExists();
    }
}
