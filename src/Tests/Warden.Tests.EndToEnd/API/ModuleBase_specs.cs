using System;
using System.Net.Http;
using System.Threading.Tasks;
using Warden.Common.Extensions;
using Warden.Tests.EndToEnd.Framework;

namespace Warden.Tests.EndToEnd.API
{
    public abstract class ModuleBase_specs
    {
        protected static Auth0SignInResponse Auth0SignInResponse;
        protected static IHttpClient HttpClient = new CustomHttpClient("http://localhost:5000");

        protected static IAuth0Client Auth0Client = new Auth0Client("warden.eu.auth0.com",
            "MjJQ06DjPwQWeXbblLHkwYXrgPBvsHwi");

        protected static Task<Auth0SignInResponse> GetAuth0SignInResponseAsync()
            => Auth0Client.SignInAsync("warden-user1@mailinator.com", "test1234");

        protected static async Task SignInToAuth0Async()
        {
            Auth0SignInResponse = await GetAuth0SignInResponseAsync();
        }

        protected static async Task<HttpResponseMessage> RequestAuthenticatedAsync(
                Func<IHttpClient, Task<HttpResponseMessage>> request)
            => await RequestAuthenticatedAsync<HttpResponseMessage>(request);

        protected static async Task<T> RequestAuthenticatedAsync<T>(Func<IHttpClient, Task<T>> request)
        {
            if (Auth0SignInResponse == null || Auth0SignInResponse.AccessToken.Empty())
                await SignInToAuth0Async();

            HttpClient.SetHeader("Authorization", $"Bearer {Auth0SignInResponse.IdToken}");

            return await request(HttpClient);
        }
    }
}