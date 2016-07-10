using Newtonsoft.Json;
using Warden.Api.Core.Domain;

namespace Warden.Api.Infrastructure.DTO
{
    public class ExceptionDto
    {
        public string Message { get; set; }
        public string Source { get; set; }

        [JsonProperty("StackTraceString")]
        public string StackTrace { get; set; }

        public ExceptionDto InnerException { get; set; }

        public ExceptionDto()
        {
        }

        public ExceptionDto(ExceptionInfo exception)
        {
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
            InnerException = exception.InnerException == null ? null : new ExceptionDto(exception.InnerException);
        }
    }
}