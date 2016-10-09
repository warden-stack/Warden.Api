using Warden.DTO.ApiKeys;

namespace Warden.Services.Storage.Mappers
{
    public class ApiKeyCollectionMapper : CollectionMapper<ApiKeyDto>
    {
        public ApiKeyCollectionMapper(IMapper<ApiKeyDto> mapper) : base(mapper)
        {
        }
    }
}