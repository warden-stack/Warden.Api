using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Warden.Api.Controllers.ControllerBase;
using System.Linq;
using Warden.Api.Core.Extensions;
using Warden.Api.Infrastructure.Commands;

namespace Warden.Api.Framework.Handlers
{
    public class RequestHandler<T> : RequestHandlerBase<T>
    {
        private readonly List<Action<T>> _actions = new List<Action<T>>();
        private readonly List<Func<T, Task>> _executeTasks = new List<Func<T, Task>>();

        public RequestHandler(ControllerBase controller, T command) : base(controller, command)
        {
        }

        public RequestHandler<T> Execute(Action<T> action)
        {
            _actions.Add(action);
            return this;
        }

        public RequestHandler<T> ExecuteAsync(Func<T, Task> task)
        {
            _executeTasks.Add(task);
            return this;
        }

        public RequestHandler<T> Always(Action<T> action)
        {
            AlwaysAction = action;
            return this;
        }

        public RequestHandler<T> OnComplete(Func<T, IActionResult> func)
        {
            OnCompleteFunc = func;
            return this;
        }

        public RequestHandler<T> OnCompleteAsync(Func<T, Task<IActionResult>> task)
        {
            OnCompleteFuncAsync = task;
            return this;
        }

        public RequestHandler<T> OnSuccess(Func<T, IActionResult> func)
        {
            OnSuccessFunc = func;
            return this;
        }

        public RequestHandler<T> OnSuccessAsync(Func<T, Task<IActionResult>> task)
        {
            OnSuccessFuncAsync = task;
            return this;
        }

        public RequestHandler<T> OnFailure(Func<T, IActionResult> func)
        {
            OnFailureFunc = func;
            return this;
        }

        public RequestHandler<T> OnFailure(Func<T, Exception, IActionResult> func)
        {
            OnFailureWithException = func;
            return this;
        }

        public RequestHandler<T> Authorize()
        {
            ShouldAuthorize = true;
            return this;
        }

        public IActionResult Handle()
        {
            var isValid = Controller.ModelState.IsValid;
            if (!isValid)
                return OnError();

            var isSuccessful = Execute();

            return isSuccessful ? OnSuccess() : OnError();
        }

        public async Task<IActionResult> HandleAsync()
        {
            AlwaysAction?.Invoke(Model);

            if (ShouldAuthorize)
                await AuthorizeAsync();

            var isValid = Controller.ModelState.IsValid;
            if (!isValid)
            {
                if (OnCompleteFunc != null)
                    return OnCompleteFunc(Model);
                if (OnCompleteFuncAsync != null)
                    return await OnCompleteFuncAsync(Model);

                return OnError();
            }

            var isSuccessful = await ExecuteAsync();
            if (isSuccessful)
            {
                //Do something useful
            }
            else
            {
                if (OnCompleteFunc == null && OnCompleteFuncAsync == null)
                    return OnError();

                if (OnCompleteFunc != null)
                    OnCompleteFunc(Model);
                else if (OnCompleteFuncAsync != null)
                    await OnCompleteFuncAsync(Model);
            }

            if (OnCompleteFunc != null)
                return OnCompleteFunc(Model);
            if (OnCompleteFuncAsync != null)
                return await OnCompleteFuncAsync(Model);

            if (OnSuccessFunc != null)
                return OnSuccessFunc(Model);
            if (OnSuccessFuncAsync != null)
                return await OnSuccessFuncAsync(Model);

            throw new Exception("No success action handler specified.");
        }

        private IActionResult OnSuccess()
        {
            return OnSuccessFunc(Model);
        }

        private IActionResult OnError()
        {
            if (OnFailureFunc != null && (Exception == null || OnFailureWithException == null))
            {
                return OnFailureFunc(Model);
            }

            return OnFailureWithException(Model, Exception);
        }

        private bool Execute()
        {
            var isSuccessful = false;
            try
            {
                foreach (var action in _actions)
                {
                    action(Model);
                }

                isSuccessful = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                Controller.HandleException(ex);
            }

            return isSuccessful;
        }

        private async Task<bool> ExecuteAsync()
        {
            var isSuccessful = false;
            try
            {
                foreach (var task in _executeTasks)
                {
                    await task(Model);
                }

                isSuccessful = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                Controller.HandleException(ex);
            }

            return isSuccessful;
        }

        private async Task AuthorizeAsync()
        {
            if (Model is IAuthenticatedCommand)
            {
                var currentUser = await Controller.GetCurrentUser();
                ((IAuthenticatedCommand)Model).AuthenticatedUserId = currentUser.Id;
            }
        }
    }
}