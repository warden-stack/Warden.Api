using Warden.Common.Queries;

namespace Warden.Api.Queries
{
    public class GetNameAvailability : IQuery
    {
        public string Name { get; set; }
    }
}