using System.Collections.Generic;

namespace Warden.Api.Settings
{
    public class EmailTemplateParameterSettings
    {
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
}