using System.Net.Http;
using System.Threading.Tasks;
using Machine.Specifications;

namespace Warden.Tests.EndToEnd.API.Modules
{
    public abstract class AccountModule_specs : ModuleBase_specs
    {
        protected static HttpResponseMessage SignInResponse;

        protected static void Initialize()
        {
        }

        protected static async Task SignInAsync()
        {
            await SignInToAuth0Async();
            SignInResponse = await RequestAuthenticatedAsync(c => c.PostAsync("sign-in", new
            {
                AccessToken = Auth0SignInResponse.AccessToken
            }));
        }
    }

    [Subject("Auth0 sign in")]
    public class when_signing_in_to_auth0 : AccountModule_specs
    {
        Establish context = () => Initialize();

        Because of = () => SignInToAuth0Async().GetAwaiter().GetResult();

        It should_return_successful_auth0_sign_in_response = () =>
        {
            Auth0SignInResponse.ShouldNotBeNull();
            Auth0SignInResponse.AccessToken.ShouldNotBeEmpty();
            Auth0SignInResponse.IdToken.ShouldNotBeEmpty();
            Auth0SignInResponse.TokenType.ShouldNotBeEmpty();
        };
    }

    [Subject("User sign in")]
    public class when_signing_in : AccountModule_specs
    {
        Establish context = () => Initialize();

        Because of = () => SignInAsync().GetAwaiter().GetResult();

        It should_return_successful_sign_in_response = () =>
        {
            SignInResponse.IsSuccessStatusCode.ShouldBeTrue();
        };
    }
}