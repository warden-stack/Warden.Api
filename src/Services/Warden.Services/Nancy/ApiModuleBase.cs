using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Warden.Common.Types;
using Warden.Common.Extensions;

namespace Warden.Services.Nancy
{
    public abstract class ApiModuleBase : NancyModule
    {
        private const string PageParameter = "page";

        protected ApiModuleBase(string modulePath = "")
            : base(modulePath)
        {
        }

        protected T BindRequest<T>() where T : new()
        => Request.Body.Length == 0 && Request.Query == null ? new T() : this.Bind<T>();

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