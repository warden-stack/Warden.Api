using System;

namespace Warden.Api.Domain
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}