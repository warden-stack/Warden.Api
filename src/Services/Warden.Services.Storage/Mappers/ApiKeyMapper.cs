using Warden.DTO.ApiKeys;

namespace Warden.Services.Storage.Mappers
{
    public class ApiKeyMapper : IMapper<ApiKeyDto>
    {
        public ApiKeyDto Map(dynamic source)
            => new ApiKeyDto
            {
                Id = source.id,
                UserId = source.userId,
                Key = source.key
            };
    }
}