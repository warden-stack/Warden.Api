using System;

namespace Warden.DTO.Operations
{
    public class OperationDto
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string UserId { get; set; }
        public string Origin { get; set; }
        public string Resource { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}