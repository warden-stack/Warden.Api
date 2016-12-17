namespace Warden.Api.Authentication
{
    public class JwtTokenSettings
    {
        public string SecretKey { get; set; }
        public int ExpiryDays { get; set; }
    }
}