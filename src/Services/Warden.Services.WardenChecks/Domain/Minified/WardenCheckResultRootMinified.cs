using Warden.Common.Domain;

namespace Warden.Services.WardenChecks.Domain.Minified
{
    public class WardenCheckResultRootMinified : IdentifiableEntity
    {
        public string o { get; set; }
        public string w { get; set; }
        public long c { get; set; }
        public WardenCheckResultMinified r { get; set; }

        public WardenCheckResultRootMinified()
        {
        }

        public WardenCheckResultRootMinified(WardenCheckResultRoot root)
        {
            o = root.OrganizationId.ToString("N");
            w = root.WardenId.ToString("N");
            c = root.CreatedAt.Ticks;
            r = new WardenCheckResultMinified(root.Result);
        }
    }
}