using FilmStudioApiManagementApp.Data;
using FilmStudioApiManagementApp.Services.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudioApiManagementApp.Models.AppUser.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public AppUser Authenticate(string username, string password)
        {
            var user = dbContext.AppUsers.Include(p => p.Password == password).FirstOrDefault(U => U.UserName == username);

            if (user == null) return null;

            return user;
        }

        public AppUser Create(AppUser user)
        {
            dbContext.AppUsers.Add(user);
            return user;
        }

        public IEnumerable<AppUser> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public AppUser GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool userExists()
        {
            throw new System.NotImplementedException();
        }
    }
}
