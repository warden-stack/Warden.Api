using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
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
        private const string PageParameter = "page";
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
                return new CommandRequestHandler<T>(CommandDispatcher, command, Response);

            var userId = GetUserIdFromApiKey();
            if (userId.Empty())
            {
                this.RequiresAuthentication();
                userId = CurrentUserId;
            }
            authenticatedCommand.UserId = userId;

            return new CommandRequestHandler<T>(CommandDispatcher, command, Response);
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

        protected T BindAuthenticatedCommand<T>() where T : IAuthenticatedCommand, new()
        {
            this.RequiresAuthentication();
            var command = BindRequest<T>();
            command.UserId = CurrentUserId;

            return command;
        }

        protected T BindRequest<T>() where T : new()
        => Request.Body.Length == 0 && Request.Query == null
            ? new T()
            : this.Bind<T>(new BindingConfig(), blacklistedProperties: "UserId");

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

        protected Negotiator FromPagedResult<T>(Maybe<PagedResult<T>> result)
        {
            if (result.HasNoValue)
                return Negotiate.WithModel(new List<T>());

            return Negotiate.WithModel(result.Value.Items)
                .WithHeader("Link", GetLinkHeader(result.Value))
                .WithHeader("X-Total-Count", result.Value.TotalResults.ToString());
        }

        private string GetLinkHeader(PagedResultBase result)
        {
            var first = GetPageLink(result.CurrentPage, 1);
            var last = GetPageLink(result.CurrentPage, result.TotalPages);
            var prev = string.Empty;
            var next = string.Empty;
            if (result.CurrentPage > 1 && result.CurrentPage <= result.TotalPages)
                prev = GetPageLink(result.CurrentPage, result.CurrentPage - 1);
            if (result.CurrentPage < result.TotalPages)
                next = GetPageLink(result.CurrentPage, result.CurrentPage + 1);

            return $"{FormatLink(next, "next")}{FormatLink(last, "last")}" +
                   $"{FormatLink(first, "first")}{FormatLink(prev, "prev")}";
        }

        private string GetPageLink(int currentPage, int page)
        {
            var url = Request.Url.ToString();
            var sign = Request.Url.Query.Empty() ? "&" : "?";
            var pageArg = $"{PageParameter}={page}";
            var link = url.Contains($"{PageParameter}=")
                ? url.Replace($"{PageParameter}={currentPage}", pageArg)
                : url += $"{sign}{pageArg}";

            return link;
        }

        private string FormatLink(string url, string rel)
            => url.Empty() ? string.Empty : $"<{url}>; rel=\"{rel}\",";
    }
}