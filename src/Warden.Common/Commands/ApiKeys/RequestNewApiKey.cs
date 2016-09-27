namespace Warden.Common.Commands.ApiKeys
{
    public class RequestNewApiKey : IFeatureRequestCommand
    {
        public string UserId { get; set; }
    }
}