using System;

namespace Warden.Api.Core.Domain
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}