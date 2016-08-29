namespace Warden.Api.Core.Domain.Wardens
{
    public class WardenConfiguration : IdentifiableEntity
    {
        public string Configuration { get; protected set; }

        protected WardenConfiguration()
        {
        }

        public WardenConfiguration(string configuration)
        {
            Configuration = configuration;
        }
    }
}