using FilmStudioApiManagementApp.Models.AppUser;
using FilmStudioApiManagementApp.Models.Film;
using FilmStudioApiManagementApp.Models.FilmStudio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FilmStudioApiManagementApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<FilmStudio> FilmStudios { get; set; }
        public DbSet<IdentityRole> IdentityRoles{ get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Film>();
        }
    }
}
