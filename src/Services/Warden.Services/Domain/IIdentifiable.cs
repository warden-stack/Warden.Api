using System;

namespace Warden.Services.Domain
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}