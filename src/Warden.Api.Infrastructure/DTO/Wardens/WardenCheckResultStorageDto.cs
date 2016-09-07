using System;

namespace Warden.Api.Infrastructure.DTO.Wardens
{
    public class WardenCheckResultStorageDto
    {
        public string WardenId { get; set; }
        public DateTime CreatedAt { get; set; }
        public WardenCheckResultDto Check { get; set; }
    }
}