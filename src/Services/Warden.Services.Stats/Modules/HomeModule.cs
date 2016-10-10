using Nancy;

namespace Warden.Services.Stats.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "Warden.Services.Stats" }));
        }
    }
}