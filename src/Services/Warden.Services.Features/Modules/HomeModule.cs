using Nancy;

namespace Warden.Services.Features.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => Response.AsJson(new { name = "Warden.Services.Features" }));
        }
    }
}