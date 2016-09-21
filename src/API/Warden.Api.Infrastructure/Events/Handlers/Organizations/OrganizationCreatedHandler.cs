using System.Threading.Tasks;
using Warden.Api.Core.Events.Organizations;

namespace Warden.Api.Infrastructure.Events.Handlers.Organizations
{
    public class OrganizationCreatedHandler : IEventHandler<OrganizationCreated>
    {
        public async Task HandleAsync(OrganizationCreated @event)
        {
        }
    }
}