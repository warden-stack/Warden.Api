using System;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Warden.Api.Core.Commands;
using Warden.Api.Core.Services;
using Warden.Api.Framework;
using Warden.Common.Commands;
using Warden.Common.Extensions;
using Warden.Common.Queries;
using Warden.Common.Types;

namespace Warden.Api.Modules
{
    public abstract class ModuleBase : NancyModule
    {
        private string _currentUserId;
        private const string ApiKeyHeader = "x-api-key";
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IIdentityProvider IdentityProvider;

        protected ModuleBase(ICommandDispatcher commandDispatcher, 
            IIdentityProvider identityProvider, 
            string modulePath = "")
            : base(modulePath)
        {
            CommandDispatcher = commandDispatcher;
            IdentityProvider = identityProvider;
        }

        protected CommandRequestHandler<T> For<T>() where T : ICommand, new()
        {
            var command = BindRequest<T>();
            var authenticatedCommand = command as IAuthenticatedCommand;
            if (authenticatedCommand == null)
                return new CommandRequestHandler<T>(CommandDispatcher, command, Response, Negotiate, Request.Url);

            var userId = GetUserIdFromApiKey();
            if (userId.Empty())
            {
                this.RequiresAuthentication();
                userId = CurrentUserId;
            }
            authenticatedCommand.UserId = userId;

            return new CommandRequestHandler<T>(CommandDispatcher, command, Response, Negotiate, Request.Url);
        }

        private string GetUserIdFromApiKey()
        {
            var apiKeyHeader = Request.Headers.FirstOrDefault(x => x.Key.EqualsCaseInvariant(ApiKeyHeader));
            if (apiKeyHeader.Key.Empty() || apiKeyHeader.Value == null || !apiKeyHeader.Value.Any())
                return string.Empty;

            return IdentityProvider.GetUserIdForApiKey(apiKeyHeader.Value.First());
        }

        protected FetchRequestHandler<TQuery, TResult> Fetch<TQuery, TResult>(Func<TQuery, Task<Maybe<TResult>>> fetch)
            where TQuery : IQuery, new() where TResult : class
        {
            var query = BindRequest<TQuery>();
            var authenticatedQuery = query as IAuthenticatedQuery;
            if (authenticatedQuery == null)
                return new FetchRequestHandler<TQuery, TResult>(query, fetch, Negotiate, Request.Url);

            var userId = GetUserIdFromApiKey();
            if (userId.Empty())
            {
                this.RequiresAuthentication();
                userId = CurrentUserId;
            }
            authenticatedQuery.UserId = userId;

            return new FetchRequestHandler<TQuery, TResult>(query, fetch, Negotiate, Request.Url);
        }

        protected FetchRequestHandler<TQuery, TResult> FetchCollection<TQuery, TResult>(
            Func<TQuery, Task<Maybe<PagedResult<TResult>>>> fetch)
            where TQuery : IPagedQuery, new() where TResult : class
        {
            var query = BindRequest<TQuery>();
            var authenticatedQuery = query as IAuthenticatedPagedQuery;
            if (authenticatedQuery == null)
                return new FetchRequestHandler<TQuery, TResult>(query, fetch, Negotiate, Request.Url);

            var userId = GetUserIdFromApiKey();
            if (userId.Empty())
            {
                this.RequiresAuthentication();
                userId = CurrentUserId;
            }
            authenticatedQuery.UserId = userId;

            return new FetchRequestHandler<TQuery, TResult>(query, fetch, Negotiate, Request.Url);
        }

        protected T BindRequest<T>() where T : new()
        => Request.Body.Length == 0 && Request.Query == null
            ? new T()
            : this.Bind<T>(new BindingConfig(), "UserId", "OperationId");

        protected string CurrentUserId
        {
            get
            {
                if (_currentUserId.Empty())
                    SetCurrentUserId(Context.CurrentUser?.Identity?.Name);

                return _currentUserId;
            }
        }

        protected void SetCurrentUserId(string id)
        {
            _currentUserId = id;
        }
    }
}