using Nancy;

namespace Warden.Services.Users.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "Warden.Services.Users" }));
        }
    }
}