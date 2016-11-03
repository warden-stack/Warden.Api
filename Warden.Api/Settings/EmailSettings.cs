using System.Collections.Generic;

namespace Warden.Api.Settings
{
    public class EmailSettings
    {
        public bool Enabled { get; set; }
        public string ApiKey { get; set; }
        public string NoReplyAccount { get; set; }
        public List<EmailTemplateSettings> Templates { get; set; }
    }
}