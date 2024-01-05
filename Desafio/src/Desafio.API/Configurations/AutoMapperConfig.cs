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
        }
    }
}
