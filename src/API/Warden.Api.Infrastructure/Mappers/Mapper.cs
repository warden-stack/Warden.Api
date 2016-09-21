using System.Linq;
using AutoMapper;
using Warden.Api.Core.Domain.Organizations;
using Warden.Api.Core.Domain.Users;
using Warden.Api.Infrastructure.Commands.Organizations;
using Warden.Api.Infrastructure.Commands.Wardens;
using Warden.Common.Dto.ApiKeys;
using Warden.Common.Dto.Organizations;
using Warden.Common.Dto.Users;
using Warden.Common.Dto.Wardens;

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
                    .ForMember(t => t.Users, s => s.MapFrom(x => x.Users.Select(u => new UserInOrganizationDto
                    {
                        CreatedAt = u.CreatedAt,
                        Email = u.Email,
                        Id = u.Id,
                        Role = u.Role.ToString()
                    })));
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