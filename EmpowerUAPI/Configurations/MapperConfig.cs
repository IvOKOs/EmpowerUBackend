using AutoMapper;
using EmpowerEFCore.Domain;
using EmpowerUAPI.Dtos;

namespace EmpowerUAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();
        }
    }
}
