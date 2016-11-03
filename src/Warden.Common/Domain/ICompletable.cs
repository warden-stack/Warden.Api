using System;

namespace Warden.Common.Domain
{
    public interface ICompletable
    {
        DateTime CompletedAt { get; }
    }
}