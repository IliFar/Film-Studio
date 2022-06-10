
using AutoMapper;
using FilmStudioApiManagementApp.Models.Film;
using FilmStudioApiManagementApp.Models.FilmStudio;
using FilmStudioApiManagementApp.Services.Film;
using FilmStudioApiManagementApp.Services.FilmStudio;

namespace FilmStudioApiManagementApp.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Film, CreateFilm>().ReverseMap();
            CreateMap<Film, FilmService>().ReverseMap();
            CreateMap<FilmStudio, FilmStudioService>().ReverseMap();
        }
    }
}
