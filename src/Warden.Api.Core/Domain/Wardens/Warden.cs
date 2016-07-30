using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Api.Core.Domain.Exceptions;
using Warden.Api.Core.Domain.Watchers;
using Warden.Api.Core.Events.Wardens;
using Warden.Api.Core.Extensions;

namespace Warden.Api.Core.Domain.Wardens
{
    public class Warden : IdentifiableEntity, ITimestampable
    {
        private HashSet<Watcher> _watchers = new HashSet<Watcher>();

        public string Name { get; protected set; }
        public bool Enabled { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public IEnumerable<Watcher> Watchers
        {
            get { return _watchers; }
            protected set { _watchers = new HashSet<Watcher>(value); }
        }

        protected Warden()
        {
        }

        public Warden(string name, bool enabled = true)
        {
            SetName(name);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            if (enabled)
                Enable();

            AddEvent(new WardenCreated(name));
        }

        public void SetName(string name)
        {
            if (name.Empty())
                throw new DomainException("Warden name can not be empty.");

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Enable()
        {
            if (Enabled)
                return;

            Enabled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Disable()
        {
            if (!Enabled)
                return;

            Enabled = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddWatcher(string name, WatcherType type)
        {
            if (name.Empty())
                throw new DomainException("Can not add a watcher without a name to the Warden.");

            var watcher = GetWatcherByName(name);
            if (watcher != null)
                throw new DomainException($"Watcher with name: '{name}' has been already added.");

            _watchers.Add(Watcher.Create(name, type));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveWatcher(string name)
        {
            if (name.Empty())
                throw new DomainException("Can not remove a watcher without a name from the Warden.");

            var watcher = GetWatcherByNameOrFail(name);
            _watchers.Remove(watcher);
            UpdatedAt = DateTime.UtcNow;
        }

        public Watcher GetWatcherByNameOrFail(string name)
        {
            if (name.Empty())
                throw new DomainException("Watcher name can not be empty.");

            var watcher = GetWatcherByName(name);
            if (watcher == null)
                throw new DomainException($"Watcher with name: '{name}' has not been found.");

            return watcher;
        }

        public Watcher GetWatcherByName(string name) => Watchers.FirstOrDefault(x => x.Name.EqualsCaseInvariant(name));
    }
}