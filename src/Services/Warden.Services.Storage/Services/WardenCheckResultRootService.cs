using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.DTO.Wardens;
using Warden.Services.Storage.Repositories;

namespace Warden.Services.Storage.Services
{
    public class WardenCheckResultRootService : IWardenCheckResultRootService
    {
        private readonly IWardenCheckResultRootRepository _wardenCheckResultRootRepository;

        public WardenCheckResultRootService(IWardenCheckResultRootRepository wardenCheckResultRootRepository)
        {
            _wardenCheckResultRootRepository = wardenCheckResultRootRepository;
        }

        public async Task ValidateAndAddAsync(string userId, Guid organizationId, Guid wardenId,
            object checkResult, DateTime createdAt)
        {
            if (checkResult == null)
            {
                throw new ArgumentNullException(nameof(checkResult),
                    "Warden check result can not be null.");
            }

            var serializedResult = JsonConvert.SerializeObject(checkResult);
            var result = JsonConvert.DeserializeObject<WardenCheckResultDto>(serializedResult);
            var rootResult = new WardenCheckResultRootDto
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrganizationId = organizationId,
                WardenId = wardenId,
                CreatedAt = createdAt,
                Result = result
            };
            await _wardenCheckResultRootRepository.AddAsync(rootResult);
        }
    }
}