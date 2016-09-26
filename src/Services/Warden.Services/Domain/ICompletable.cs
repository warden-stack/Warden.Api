using System;

namespace Warden.Services.Domain
{
    public interface ICompletable
    {
        DateTime CompletedAt { get; }
    }
}