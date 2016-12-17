using System.Security.Claims;
using System.Security.Principal;

namespace Warden.Api.Authentication
{
    public class WardenIdentity : ClaimsPrincipal
    {
        public WardenIdentity(string name) : base(new GenericIdentity(name))
        {
        }
    }
}