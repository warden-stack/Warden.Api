namespace Warden.Services.WardenChecks.Domain.Minified
{
    public class ExceptionInfoMinified
    {
        public string m { get; set; }
        public string s { get; set; }
        public string t { get; set; }
        public ExceptionInfoMinified i { get; set; }

        public ExceptionInfoMinified()
        {
        }

        public ExceptionInfoMinified(ExceptionInfo exception)
        {
            m = exception.Message;
            s = exception.Source;
            t = exception.StackTrace;
            i = exception.InnerException != null ? new ExceptionInfoMinified(exception.InnerException) : null;
        }
    }
}