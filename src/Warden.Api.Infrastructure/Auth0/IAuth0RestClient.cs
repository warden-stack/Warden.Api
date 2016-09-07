using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Api.Infrastructure.Settings;

namespace Warden.Api.Infrastructure.Auth0
{
    public interface IAuth0RestClient
    {
        Task<HttpResponseMessage> CreateUserAsync(string email, string password);
    }

    public class Auth0RestClient : IAuth0RestClient
    {
        private readonly Auth0Settings _settings;
        private string BaseAddress => $"https://{_settings.Domain}/api/v2/";

        public Auth0RestClient(Auth0Settings settings)
        {
            _settings = settings;
        }

        public async Task<HttpResponseMessage> CreateUserAsync(string email, string password)
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            var data = new
            {
                Connection = _settings.Connection,
                Email = email,
                Password = password
            };
            var json = JsonConvert.SerializeObject(data);
            client.DefaultRequestHeaders.Add("Authorization", _settings.CreateUsersToken);
            var response = await client.PostAsync("users", new StringContent(json, Encoding.UTF8, "application/json") );

            return response;
        }
    }
}