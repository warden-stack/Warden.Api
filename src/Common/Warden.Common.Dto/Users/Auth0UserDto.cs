using Newtonsoft.Json;

namespace Warden.Common.Dto.Users
{
    public class Auth0UserDto
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Picture { get; set; }
    }
}