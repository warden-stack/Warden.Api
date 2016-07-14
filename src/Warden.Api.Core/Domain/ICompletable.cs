using System;

namespace Warden.Api.Core.Domain
{
    public interface ICompletable
    {
        DateTime CompletedAt { get; }
    }
}