using Microsoft.Extensions.Configuration;

namespace Warden.Services.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration, string section) where T : new()
        {
            var configurationValue = new T();
            configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}