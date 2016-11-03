using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Common.Domain;
using Warden.Common.Extensions;

namespace Warden.Services.Organizations.Domain
{
    public class Warden : ITimestampable
    {
        private HashSet<Watcher> _watchers = new HashSet<Watcher>();
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string OwnerId { get; protected set; }
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

        public Warden(Guid id, User owner, string name, bool enabled = true)
        {
            Id = id;
            OwnerId = owner.UserId;
            SetName(name);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            if (enabled)
                Enable();

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

        public void AddWatcher(string name, string type)
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