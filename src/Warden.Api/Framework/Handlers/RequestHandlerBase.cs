using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Warden.Api.Controllers.ControllerBase;

namespace Warden.Api.Framework.Handlers
{
    public abstract class RequestHandlerBase<T>
    {
        protected readonly ControllerBase Controller;
        protected readonly T Model;
        protected Exception Exception;
        protected Func<T, Exception, IActionResult> OnFailureWithException;
        protected Action<T> AlwaysAction;
        protected Func<T, IActionResult> OnCompleteFunc;
        protected Func<T, Task<IActionResult>> OnCompleteFuncAsync;
        protected Func<T, IActionResult> OnFailureFunc;
        protected Func<T, IActionResult> OnSuccessFunc;
        protected Func<T, Task<IActionResult>> OnSuccessFuncAsync;
        protected bool ShouldAuthorize;

        protected RequestHandlerBase(ControllerBase controller, T model)
        {
            Controller = controller;
            Model = model;
        }
    }
}