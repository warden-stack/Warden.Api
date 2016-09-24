using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;

namespace Warden.Services.Host
{
    public interface IServiceHost
    {
        Task RunAsync();
    }

    public class ServiceHost : IServiceHost
    {
        public async Task RunAsync()
        {
            throw new System.NotImplementedException();
        }

        public static Builder Create(string name)
        {
            var builder = new Builder();

            return builder;
        }

        public class Builder
        {
            public Builder Create()
            {
                return this;
            }

            public BusBuilder WithBus()
            {
                return new BusBuilder();
            }

            public ServiceHost Build()
            {
                return new ServiceHost();
            }
        }

        public class BusBuilder : Builder
        {
            public BusBuilder WithCommandHandler<TCommand>() where TCommand : ICommand
            {
                return this;
            }

            public BusBuilder WithEventHandler()
            {
                return this;
            }
        }

        private static T GetConfigurationValue<T>(string section) where T : new()
        {
            var env = "dev";
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var configurationValue = new T();
            configurationRoot.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}