using System;
using Microsoft.AspNet.SignalR;

namespace Warden.Services.RealTime
{
    public interface ISignalRService
    {
        void SendCheckResultSaved(Guid organizationId, Guid wardenId, object checkResult);
    }

    public class SignalRService : ISignalRService
    {
        private readonly IHubContext _hub;

        public SignalRService(IHubContext hub)
        {
            _hub = hub;
        }

        public void SendCheckResultSaved(Guid organizationId, Guid wardenId, object checkResult)
        {
            var groupName = GetWardenGroupName(organizationId, wardenId);
            _hub.Clients.Group(groupName).checkSaved(checkResult);
        }

        private static string GetWardenGroupName(Guid organizationId, Guid wardenId)
            => $"{organizationId:N}:{wardenId:N}";
    }
}