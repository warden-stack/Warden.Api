using System.Collections.Generic;

namespace Warden.Api.Core.Settings
{
    public class EmailTemplateParameterSettings
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}