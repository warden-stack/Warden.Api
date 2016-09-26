using System;

namespace Warden.Services.Domain
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}