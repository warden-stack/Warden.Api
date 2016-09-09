using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Api.Infrastructure.DTO.Users;
using Warden.Api.Infrastructure.Settings;

namespace Warden.Api.Infrastructure.Auth0
{
    public interface IAuth0RestClient
    {
        Task<Auth0User> GetUserAsync(string externalId);
    }

    public class Auth0RestClient : IAuth0RestClient
    {
        private readonly Auth0Settings _settings;
        private string BaseAddress => $"https://{_settings.Domain}/api/v2/";

        public Auth0RestClient(Auth0Settings settings)
        {
            _settings = settings;
        }

        public async Task<Auth0User> GetUserAsync(string externalId)
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_settings.ReadUsersToken}");
            var response = await client.GetAsync($"users/{externalId}");
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Auth0User>(content);

            return user;
        }
    }
}