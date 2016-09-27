using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Warden.Common.Types;

namespace Warden.Services.Storage.Providers
{
    public class ProviderClient : IProviderClient
    {
        public async Task<Maybe<T>> GetAsync<T>(string url, string endpoint) where T : class
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(GetBaseAddress(url)) };
            httpClient.DefaultRequestHeaders.Remove("Accept");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
                return new Maybe<T>();

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);

            return data;
        }

        private string GetBaseAddress(string url) => url.EndsWith("/") ? url : $"{url}/";
    }
}