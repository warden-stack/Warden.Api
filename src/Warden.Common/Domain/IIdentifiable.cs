using System;

namespace Warden.Common.Domain
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}