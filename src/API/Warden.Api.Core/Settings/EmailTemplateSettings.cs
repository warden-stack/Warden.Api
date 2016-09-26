using System.Collections.Generic;

namespace Warden.Api.Core.Settings
{
    public class EmailTemplateSettings
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public List<EmailTemplateParameterSettings> Parameters { get; set; }
    }
}