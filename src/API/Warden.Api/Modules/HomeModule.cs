using Nancy;

namespace Warden.Api.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => $"Warden API is running on: {Context.Request.Url}");
        }
    }
}