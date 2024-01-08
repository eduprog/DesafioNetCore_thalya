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
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UpdateUserRequest>().ReverseMap();
            CreateMap<User, UpdateLoginUserRequest>().ReverseMap();
            CreateMap<Product, InsertProductRequest>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
