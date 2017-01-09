using System;
using Warden.Common.Queries;

namespace Warden.Api.Queries
{
    public class GetOperation : IQuery
    {
        public Guid RequestId { get; set; }
    }
}