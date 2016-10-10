using Nancy;

namespace Warden.Services.Organizations.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "Warden.Services.Organizations" }));
        }
    }
}