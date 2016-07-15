using AutoMapper;
using Warden.Api.Infrastructure.Commands;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Api.Infrastructure.DTO;

namespace Warden.Api.Infrastructure.Mappers
{
    public static class Mapper
    {
        public static IMapper Resolve()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WardenDto, CreateWarden>());

            return config.CreateMapper();
        }
    }
}