using System;
using System.Collections.Generic;
using System.Linq;
using Warden.Common.Domain;
using Warden.Common.Extensions;

namespace Warden.Services.Organizations.Domain
{
    public class Organization : IdentifiableEntity, ITimestampable
    {
        private HashSet<UserInOrganization> _users = new HashSet<UserInOrganization>();
        private HashSet<Warden> _wardens = new HashSet<Warden>();

        public string Name { get; protected set; }
        public string Description { get; set; }
        public string OwnerId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public IEnumerable<UserInOrganization> Users
        {
            get { return _users; }
            protected set { _users = new HashSet<UserInOrganization>(value); }
        }

        public IEnumerable<Warden> Wardens
        {
            get { return _wardens; }
            protected set { _wardens = new HashSet<Warden>(value); }
        }

        protected Organization()
        {
        }

        public Organization(Guid id, string name, User owner, string description = "")
        {
            Id = id;
            SetName(name);
            SetOwner(owner);
            SetDescription(description);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (name.Empty())
                throw new DomainException("Organization name can not be empty.");

            Name = name.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            Description = description?.Trim() ?? string.Empty;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetOwner(User owner)
        {
            if (owner == null)
                throw new DomainException("Organization owner can not be null.");

            OwnerId = owner.UserId;
            AddUser(owner, OrganizationRoles.Owner);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddUser(User user, string role)
        {
            if (user == null)
                throw new DomainException("Can not add empty user to the organization.");

            if (Users.Any(x => x.UserId == user.UserId))
                throw new DomainException($"User '{user.UserId}' is already in the organization.");

            _users.Add(UserInOrganization.Create(user, role));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveUser(string id)
        {
            var userInOrganization = Users.FirstOrDefault(x => x.UserId == id);
            if (userInOrganization == null)
                throw new DomainException($"User with id '{id}' was not found in the organization.");
            if (OwnerId == id)
                throw new DomainException("Owner can not be removed from organization.");

            _users.Remove(userInOrganization);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddWarden(Guid id, User owner, string name, bool enabled = true)
        {
            if (name.Empty())
                throw new DomainException("Can not add a warden without a name to the organization.");

            var warden = GetWardenByName(name);
            if (warden != null)
                throw new DomainException($"Warden with name: '{name}' has been already added.");

            warden = new Warden(id, owner, name,  enabled);
            _wardens.Add(warden);
            UpdatedAt = DateTime.UtcNow;
        }

        public void EditWarden(string name, string newName)
        {
            var warden = GetWardenByNameOrFail(name);
            var existingWarden = GetWardenByName(newName);
            if(existingWarden != null && existingWarden.Id != warden.Id)
                throw new DomainException($"Warden with name: '{newName}' already exists.");

            warden.SetName(newName);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveWarden(string name)
        {
            if (name.Empty())
                throw new DomainException("Can not remove a warden without a name from the organization.");

            var warden = GetWardenByNameOrFail(name);
            _wardens.Remove(warden);
            UpdatedAt = DateTime.UtcNow;
        }

        public void EnableWarden(string name)
        {
            var warden = GetWardenByNameOrFail(name);
            warden.Enable();
            UpdatedAt = DateTime.UtcNow;
        }

        public void DisableWarden(string name)
        {
            var warden = GetWardenByNameOrFail(name);
            warden.Disable();
            UpdatedAt = DateTime.UtcNow;
        }

        public Warden GetWardenByNameOrFail(string name)
        {
            if (name.Empty())
                throw new DomainException("Warden name can not be empty.");

            var warden = GetWardenByName(name);
            if (warden == null)
                throw new DomainException($"Warden with name: '{name}' has not been found.");

            return warden;
        }

        public Warden GetWardenByName(string name) 
            => Wardens.FirstOrDefault(x => x.Name.EqualsCaseInvariant(name));

        public Warden GetWardenById(Guid id)
            => Wardens.FirstOrDefault(x => x.Id.Equals(id));
    }
}