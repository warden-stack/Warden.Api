using Newtonsoft.Json;

namespace Warden.Services.Users.Domain
{
    public class Auth0User
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Picture { get; set; }
    }
}