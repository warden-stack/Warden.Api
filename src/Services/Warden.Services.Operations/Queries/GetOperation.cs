using System;
using Warden.Common.Queries;

namespace Warden.Services.Operations.Queries
{
    public class GetOperation : IQuery
    {
        public Guid RequestId { get; set; }
    }
}