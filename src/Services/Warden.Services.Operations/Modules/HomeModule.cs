using Nancy;

namespace Warden.Services.Operations.Modules
{
    public class HomeModule : ModuleBase
    {
        public HomeModule()
        {
            Get("", args => Response.AsJson(new { name = "Warden.Services.Operations" }));
        }
    }
}