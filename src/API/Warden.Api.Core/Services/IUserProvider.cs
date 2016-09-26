using System.Threading.Tasks;
using Warden.Common.DTO.Users;

namespace Warden.Api.Core.Services
{
    public interface IUserProvider
    {
        Task<UserDto> GetAsync(string id);
    }
}