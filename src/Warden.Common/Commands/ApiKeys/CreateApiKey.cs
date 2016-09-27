namespace Warden.Common.Commands.ApiKeys
{
    public class CreateApiKey : IAuthenticatedCommand
    {
        public string AuthenticatedUserId { get; set; }
    }
}