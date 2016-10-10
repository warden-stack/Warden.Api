using Nancy;

namespace Warden.Services.Storage.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "Warden.Services.Storage" }));
        }
    }
}