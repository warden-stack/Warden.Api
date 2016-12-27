using System;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Warden.Api.Commands;
using Warden.Api.Framework;
using Warden.Api.Services;
using Warden.Api.Validation;
using Warden.Common.Commands;
using Warden.Common.Extensions;
using Warden.Common.Queries;
using Warden.Common.Types;
using Warden.Common.Nancy;

namespace Warden.Api.Modules
{
    public abstract class ModuleBase : NancyModule
    {
        private string _currentUserId;
        private readonly IValidatorResolver _validatorResolver;
        private const string ApiKeyHeader = "x-api-key";
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IIdentityProvider IdentityProvider;

        protected ModuleBase(ICommandDispatcher commandDispatcher,
            IValidatorResolver validatorResolver,
            IIdentityProvider identityProvider, 
            string modulePath = "")
            : base(modulePath)
        {
            CommandDispatcher = commandDispatcher;
            _validatorResolver = validatorResolver;
            IdentityProvider = identityProvider;
        }

        protected CommandRequestHandler<T> For<T>() where T : ICommand, new()
        {
            var command = BindRequest<T>();
            var authenticatedCommand = command as IAuthenticatedCommand;
            var culture = Request.Headers.AcceptLanguage?.FirstOrDefault()?.Item1;
            culture = culture.Empty() ? "en-gb" : culture.TrimToLower();
            if (authenticatedCommand == null)
            {
                return new CommandRequestHandler<T>(CommandDispatcher, command, Response,
                    _validatorResolver, Negotiate, Request.Url, culture);
            }

            var userId = GetUserIdFromApiKey();
            if (userId.Empty())
            {
                this.RequiresAuthentication();
                userId = CurrentUserId;
            }
            authenticatedCommand.UserId = userId;

            return new CommandRequestHandler<T>(CommandDispatcher, command, Response,
                _validatorResolver,Negotiate, Request.Url, culture);
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