using System;

namespace Warden.Common.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        string AuthenticatedUserId { get; set; }
    }
}