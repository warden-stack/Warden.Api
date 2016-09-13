using System.Linq;
using AutoMapper;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Infrastructure.Commands.Organizations;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Api.Infrastructure.DTO.ApiKeys;
using Warden.Api.Infrastructure.DTO.Organizations;
using Warden.Api.Infrastructure.DTO.Users;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Mappers
{
    public static class Mapper
    {
        public static IMapper Resolve()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WardenDto, CreateWarden>();
                cfg.CreateMap<Organization, OrganizationDto>()
                    .ForMember(t => t.Users, s => s.MapFrom(x => x.Users.Select(u => new UserInOrganizationDto(u))));
                cfg.CreateMap<OrganizationDto, EditOrganization>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Core.Domain.Wardens.Warden, WardenDto>();
                cfg.CreateMap<object, SpawnWarden>()
                    .ForMember(t => t.Configuration, s => s.MapFrom(x => x));
                cfg.CreateMap<ApiKey, ApiKeyDto>();
            });

            return config.CreateMapper();
        }
    }
}