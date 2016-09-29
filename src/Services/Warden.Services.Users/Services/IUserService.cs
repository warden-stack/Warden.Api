using System.Threading.Tasks;
using Warden.Common.Types;
using Warden.Services.Users.Domain;

namespace Warden.Services.Users.Services
{
    public interface IUserService
    {
        Task<Maybe<User>> GetAsync(string id);
        Task CreateAsync(string userId, string email, string role, bool activate = true);
    }
}