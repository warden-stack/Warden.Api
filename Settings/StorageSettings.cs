using System;

namespace Warden.Api.Settings
{
    public class StorageSettings
    {
        public string Url { get; set; }
        public TimeSpan? CacheExpiry { get; set; }
    }
}