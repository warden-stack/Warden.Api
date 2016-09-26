using System;

namespace Warden.Common.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid AuthenticatedUserId { get; set; }
    }
}