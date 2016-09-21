using Newtonsoft.Json;

namespace Warden.Common.Dto.Common
{
    public class ExceptionDto
    {
        public string Message { get; set; }
        public string Source { get; set; }

        [JsonProperty("StackTraceString")]
        public string StackTrace { get; set; }

        public ExceptionDto InnerException { get; set; }
    }
}