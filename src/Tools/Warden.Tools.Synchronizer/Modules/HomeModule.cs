using Nancy;

namespace Warden.Tools.Synchronizer.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => Response.AsJson(new { name = "Warden.Tools.Synchronizer" }));
        }
    }
}