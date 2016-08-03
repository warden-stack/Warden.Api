using System;

namespace Warden.Api.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        string AuthenticatedUserId { get; set; }
    }
}