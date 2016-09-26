namespace Warden.Common.Commands.Wardens
{
    public class RunWardenProcess : ICommand
    {
        public string ConfigurationId { get; }
        public string Token { get; }

        protected RunWardenProcess()
        {
        }

        public RunWardenProcess(string configurationId, string token)
        {
            ConfigurationId = configurationId;
            Token = token;
        }
    }
}