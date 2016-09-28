using System;

namespace Warden.Api.Core.Settings
{
    public class StorageSettings
    {
        public string Url { get; set; }
        public TimeSpan? CacheExpiry { get; set; }
    }
}