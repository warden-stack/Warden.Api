using Warden.Common.Queries;

namespace Warden.Services.Storage.Queries
{
    public class GetUser : IQuery
    {
        public string Id { get; set; }
    }
}