using System;

namespace Warden.Api.Domain
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}