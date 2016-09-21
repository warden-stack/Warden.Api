namespace Warden.Common.Services.Commands
{
    public class RunWardenProcess : ICommand
    {
        public string ConfigurationId { get; }
        public string Token { get; }

        public RunWardenProcess(string configurationId, string token)
        {
            ConfigurationId = configurationId;
            Token = token;
        }
    }
}