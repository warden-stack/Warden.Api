using System;

namespace Warden.Common.Domain
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}