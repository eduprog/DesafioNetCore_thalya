using AutoMapper;
using Desafio.Application;
using Desafio.Domain;

namespace Desafio.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Unit, UnitRequest>().ReverseMap();
            CreateMap<Unit, UnitResponse>().ReverseMap();
            CreateMap<User, LoginUserRequest>().ReverseMap();
            CreateMap<User, LoginUserResponse>().ReverseMap();
            CreateMap<User, RegisterUserRequest>().ReverseMap();
            CreateMap<User, RegisterUserResponse>().ReverseMap();
        }
    }
}
