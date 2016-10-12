using System;

namespace Warden.Common.Commands
{
    public class Request
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Origin { get; set; }
        public string Resource { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}