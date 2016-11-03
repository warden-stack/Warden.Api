using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Warden.Common.Extensions;
using Warden.DTO.Operations;
using Warden.Common.Mongo;

namespace Warden.Services.Storage.Repositories.Queries
{
    public static class OperationQueries
    {
        public static IMongoCollection<OperationDto> Operations(this IMongoDatabase database)
            => database.GetCollection<OperationDto>();

        public static async Task<OperationDto> GetByRequestIdAsync(this IMongoCollection<OperationDto> operations,
            Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await operations.AsQueryable().FirstOrDefaultAsync(x => x.RequestId == id);
        }
    }
}