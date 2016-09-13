using System;
using Microsoft.AspNet.SignalR;
using Warden.Api.Infrastructure.DTO.Wardens;

namespace Warden.Api.Infrastructure.Services
{
    public interface ISignalRService
    {
        void SendCheckResultSaved(Guid organizationId, Guid wardenId, WardenCheckResultDto checkResult);
    }

    public class SignalRService : ISignalRService
    {
        private readonly IHubContext _hub;

        public SignalRService(IHubContext hub)
        {
            _hub = hub;
        }

        public void SendCheckResultSaved(Guid organizationId, Guid wardenId, WardenCheckResultDto checkResult)
        {
            var groupName = GetWardenGroupName(organizationId, wardenId);
            _hub.Clients.Group(groupName).checkSaved(checkResult);
        }

        private static string GetWardenGroupName(Guid organizationId, Guid wardenId)
            => $"{organizationId.ToString("N")}:{wardenId.ToString("N")}";
    }
}