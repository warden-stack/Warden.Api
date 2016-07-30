using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Warden.Api.Framework.Filters
{
    public class ExceptionFilter : ActionFilterAttribute, IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception == null)
                return;

            await WriteExceptionAsync(context, exception, HttpStatusCode.BadRequest);
        }

        private static async Task WriteExceptionAsync(ActionContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = (int)code;
            await response.WriteAsync(exception.Message);
        }
    }
}