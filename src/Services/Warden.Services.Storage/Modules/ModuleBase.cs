using Warden.Common.Nancy;

namespace Warden.Services.Storage.Modules
{
    public abstract class ModuleBase : ApiModuleBase
    {
        protected ModuleBase(string modulePath = "") : base(modulePath)
        {
        }
    }
}