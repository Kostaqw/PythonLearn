using AutoMapper;
using PythonLearn.DAL.other;
using PythonLearn.Domain.Entity;
using PythonLearn.Domain.ViewModel.User;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Mime;

namespace PythonLearn.Models.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.AboutMe, opt => opt.MapFrom(src => src.AboutMe))
                .ForMember(dest => dest.avatar, opt => opt.MapFrom(src => ImageProcess.ConverToFormFile(src.avatar)))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.avatar))
                ;
        }

    }
}
