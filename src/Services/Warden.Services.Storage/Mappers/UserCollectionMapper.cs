using Warden.DTO.Users;

namespace Warden.Services.Storage.Mappers
{
    public class UserCollectionMapper : CollectionMapper<UserDto>
    {
        public UserCollectionMapper(IMapper<UserDto> mapper) : base(mapper)
        {
        }
    }
}