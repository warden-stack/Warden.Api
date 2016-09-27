namespace Warden.Common.Commands.ApiKeys
{
    public class CreateApiKey : IAuthenticatedCommand
    {
        public string UserId { get; set; }
    }
}