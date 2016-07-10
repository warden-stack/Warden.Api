using Microsoft.AspNetCore.Mvc.Filters;

namespace Warden.Api.Framework.Filters
{
    public class ExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
        }
    }
}