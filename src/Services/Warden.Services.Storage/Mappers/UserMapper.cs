using Warden.DTO.Users;

namespace Warden.Services.Storage.Mappers
{
    public class UserMapper : IMapper<UserDto>
    {
        public UserDto Map(dynamic source)
            => new UserDto
            {
                UserId = source.userId,
                Email = source.email,
                Role = source.role,
                CreatedAt = source.createdAt,
            };
    }
}