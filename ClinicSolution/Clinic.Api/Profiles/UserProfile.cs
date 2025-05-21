using AutoMapper;
using Clinic.Domain.Models.DTOs;
using Clinic.Domain.Models.Entities;

namespace Clinic.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        { 
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
