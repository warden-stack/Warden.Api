using Warden.Common.Queries;

namespace Warden.Services.Users.Queries
{
    public class GetUser : IQuery
    {
        public string Id { get; set; }
    }
}