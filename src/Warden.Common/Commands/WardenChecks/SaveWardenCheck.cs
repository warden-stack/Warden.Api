using System;
using System.Threading.Tasks;
using Warden.Common.DTO.Wardens;

namespace Warden.Common.Commands.WardenChecks
{
    public class SaveWardenCheck : ICommand
    {
        public Guid AuthenticatedUserId { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public WardenCheckResultDto Check { get; set; }
    }


}