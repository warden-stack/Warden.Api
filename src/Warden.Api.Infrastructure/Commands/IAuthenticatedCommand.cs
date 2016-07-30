using System;

namespace Warden.Api.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid AuthenticatedUserId { get; set; }
    }
}