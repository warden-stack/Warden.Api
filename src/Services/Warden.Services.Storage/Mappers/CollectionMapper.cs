using System.Collections.Generic;
using Newtonsoft.Json;

namespace Warden.Services.Storage.Mappers
{
    public class CollectionMapper<T> : IMapper<IEnumerable<T>>
    {
        public IEnumerable<T> Map(dynamic source)
        {
            var json = JsonConvert.SerializeObject(source);
            var collection = JsonConvert.DeserializeObject<IEnumerable<T>>(json);

            return collection;
        }
    }
}