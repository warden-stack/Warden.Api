using System.Threading.Tasks;
using Warden.Services.WardenChecks.Domain;
using Warden.Services.WardenChecks.Domain.Minified;
using Warden.Services.WardenChecks.Repositories;

namespace Warden.Services.WardenChecks.Services
{
    public class WardenCheckStorage : IWardenCheckStorage
    {
        private readonly IWardenCheckResultRootMinifiedRepository _wardenCheckResultRootMinifiedRepository;

        public WardenCheckStorage(IWardenCheckResultRootMinifiedRepository wardenCheckResultRootMinifiedRepository)
        {
            _wardenCheckResultRootMinifiedRepository = wardenCheckResultRootMinifiedRepository;
        }

        public async Task SaveAsync(WardenCheckResultRoot checkResult)
            => await _wardenCheckResultRootMinifiedRepository
                .AddAsync(new WardenCheckResultRootMinified(checkResult));
    }
}