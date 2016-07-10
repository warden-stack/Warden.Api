using System;

namespace Warden.Api.Core.Domain
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}