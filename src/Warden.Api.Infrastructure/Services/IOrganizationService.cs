using System;
using System.Threading.Tasks;

namespace Warden.Api.Infrastructure.Services
{
    public interface IOrganizationService
    {
        Task CreateAsync(string userId, string name);
    }
}