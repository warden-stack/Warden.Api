using Nancy;
using RethinkDb.Driver.Ast;

namespace Warden.Services.Storage.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", args => Response.AsJson(new { name = "Warden.Services.Storage" }));
        }
    }
}