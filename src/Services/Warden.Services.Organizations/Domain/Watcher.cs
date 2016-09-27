using System;
using Warden.Common.Extensions;

namespace Warden.Services.Organizations.Domain
{
    public class Watcher
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }

        protected Watcher()
        {
        }

        protected Watcher(string name, string type)
        {
            if (name.Empty())
                throw new ArgumentException("Watcher name can not be empty.", nameof(type));
            if (type.Empty())
                throw new ArgumentException("Watcher type can not be empty.", nameof(type));

            Name = name;
            Type = type.ToLowerInvariant();
        }

        public static Watcher Create(string name, string type)
            => new Watcher(name, type);
    }
}