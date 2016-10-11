using Newtonsoft.Json;

namespace Warden.Tests.EndToEnd.Framework
{
    public class Auth0SignInResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}