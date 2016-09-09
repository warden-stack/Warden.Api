using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Warden.Api.Hubs
{
    public class WardenHub : Hub
    {
        private readonly ConcurrentDictionary<string, string> _clients = new ConcurrentDictionary<string, string>();

        public override async Task OnConnected()
        {
            await ValidateClientAsync();
            var groupName = await ParseRequestAndGetWardenGroupNameOrFailAsync();
            await Groups.Add(Context.ConnectionId, groupName);
            await base.OnConnected();
        }

        public override async Task OnReconnected()
        {
            await ValidateClientAsync();
            var groupName = await ParseRequestAndGetWardenGroupNameOrFailAsync();
            await Groups.Add(Context.ConnectionId, groupName);
            await base.OnReconnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            RemoveClient();
            var groupName = await ParseRequestAndGetWardenGroupNameOrFailAsync();
            await Groups.Remove(Context.ConnectionId, groupName);
            await base.OnDisconnected(stopCalled);
        }

        private async Task<string> ParseRequestAndGetWardenGroupNameOrFailAsync()
        {
            var organizationId = Context.QueryString["organizationId"];
            var wardenId = Context.QueryString["wardenId"];
            if (string.IsNullOrWhiteSpace(organizationId) || string.IsNullOrWhiteSpace(wardenId))
                throw new InvalidOperationException("Empty organization id and/or warden id.");

            //Guid organizationId;
            //if (!Guid.TryParse(organizationIdValue, out organizationId))
            //    throw new InvalidOperationException("Invalid organization id.");

            //var hasAccess = await _organizationService.IsUserInOrganizationAsync(organizationId,
            //    Guid.Parse(Context.User.Identity.Name));
            //if (!hasAccess)
            //    throw new InvalidOperationException("No access to the selected organization and warden.");

            return GetWardenGroupName(organizationId, wardenId);
        }

        private async Task ValidateClientAsync()
        {
            var token = Context.QueryString["token"];
            var organizationId = Context.QueryString["organizationId"];
            var wardenId = Context.QueryString["wardenId"];
            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("Empty token.");
            if (string.IsNullOrWhiteSpace(organizationId))
                throw new InvalidOperationException("Empty organization id.");
            if (string.IsNullOrWhiteSpace(wardenId))
                throw new InvalidOperationException("Empty warden id.");

            RemoveClient();
            var clientId = GetClientId(token, organizationId, wardenId);
            _clients[Context.ConnectionId] = clientId;
        }

        private void RemoveClient()
        {
            var value = "";
            if (_clients.ContainsKey(Context.ConnectionId))
                _clients.TryRemove(Context.ConnectionId, out value);
        }

        private static string GetClientId(string token, string organizationId, string wardenId)
            => $"{token}::{GetWardenGroupName(organizationId, wardenId)}".ToLowerInvariant();

        private static string GetWardenGroupName(string organizationId, string wardenId)
            => $"{organizationId}::{wardenId}".ToLowerInvariant();
    }
}