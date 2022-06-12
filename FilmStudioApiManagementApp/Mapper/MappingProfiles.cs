
using AutoMapper;
using FilmStudioApiManagementApp.Models.AppUser;
using FilmStudioApiManagementApp.Models.Film;
using FilmStudioApiManagementApp.Models.FilmStudio;
using FilmStudioApiManagementApp.Services.AppUser;
using FilmStudioApiManagementApp.Services.Film;
using FilmStudioApiManagementApp.Services.FilmStudio;
using Microsoft.AspNetCore.Identity;

namespace FilmStudioApiManagementApp.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Film, CreateFilm>().ReverseMap();
            CreateMap<Film, FilmService>().ReverseMap();
            CreateMap<FilmStudio, FilmStudioService>().ReverseMap();
            CreateMap<AppUser, UserRegister>().ReverseMap();
            CreateMap<AppUser, UserService>().ReverseMap();
            CreateMap<AppUser, UserAuthenticate>().ReverseMap();
            CreateMap<AppUser, RegisterFilmStudio>().ReverseMap();
        }
    }
}
