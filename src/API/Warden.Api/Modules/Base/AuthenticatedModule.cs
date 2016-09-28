using Nancy.ModelBinding;
using Nancy.Security;
using Warden.Api.Core.Commands;
using Warden.Common.Commands;
using Warden.Common.Extensions;

namespace Warden.Api.Modules.Base
{
    public class AuthenticatedModule : ModuleBase
    {
        private string _currentUserId;

        public AuthenticatedModule(ICommandDispatcher commandDispatcher, string modulePath = "")
            : base(commandDispatcher, modulePath)
        {
            this.RequiresAuthentication();
        }

        protected string CurrentUserId
        {
            get
            {
                if (_currentUserId.Empty())
                    SetCurrentUserId(Context.CurrentUser?.Identity?.Name?.Replace("auth0|", string.Empty));

                return _currentUserId;
            }
        }

        protected void SetCurrentUserId(string id)
        {
            _currentUserId = id;
        }

        protected T BindAuthenticatedCommand<T>() where T : IAuthenticatedCommand, new()
        {
            if (Request.Body.Length == 0)
            {
                return new T
                {
                    UserId = CurrentUserId
                };
            }

            var command = this.Bind<T>(new BindingConfig(), blacklistedProperties: "UserId");
            command.UserId = CurrentUserId;

            return command;
        }
    }
}