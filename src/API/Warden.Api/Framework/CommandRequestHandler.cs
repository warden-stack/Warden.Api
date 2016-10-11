using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nancy;
using Nancy.Responses.Negotiation;
using Warden.Api.Core.Commands;
using Warden.Common.Commands;
using Warden.Common.Extensions;

namespace Warden.Api.Framework
{
    public class CommandRequestHandler<T> where T : ICommand
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly T _command;
        private readonly IResponseFormatter _responseFormatter;
        private readonly Negotiator _negotiator;
        private Func<T, object> _responseFunc;
        private Func<T, Task<object>> _asyncResponseFunc;
        private Guid _resourceId;

        public CommandRequestHandler(ICommandDispatcher dispatcher, T command, IResponseFormatter responseFormatter, 
            Negotiator negotiator, Url url)
        {
            _dispatcher = dispatcher;
            _command = command;
            _command.Details = new CommandDetails
            {
                Origin = url.Path.Remove(0,1),
                CreatedAt = DateTime.UtcNow
            };
            _responseFormatter = responseFormatter;
            _negotiator = negotiator;
        }

        public CommandRequestHandler<T> Set(Action<T> action)
        {
            action(_command);

            return this;
        }

        public CommandRequestHandler<T> SetResourceId(Expression<Func<T, Guid>> memberLamda)
        {
            _resourceId = Guid.NewGuid();
            _command.SetPropertyValue(memberLamda, _resourceId);

            return this;
        }

        public CommandRequestHandler<T> OnSuccess(HttpStatusCode statusCode)
        {
            _responseFunc = x => statusCode;

            return this;
        }

        public CommandRequestHandler<T> OnSuccess(Func<T, object> func)
        {
            _responseFunc = func;

            return this;
        }

        public CommandRequestHandler<T> OnSuccess(Func<T, Task<object>> func)
        {
            _asyncResponseFunc = func;

            return this;
        }

        public CommandRequestHandler<T> OnSuccessCreated(string path)
        {
            var url = string.Format(path, _resourceId.ToString("N"));
            _command.Details.Resource = url;
            return OnSuccessCreated(c => url);
        }


        public CommandRequestHandler<T> OnSuccessCreated(Func<T, string> func)
        {
            _responseFunc = x => _responseFormatter.AsRedirect(func(_command)).WithStatusCode(201).WithResourceIdHeader(_resourceId);

            return this;
        }

        public CommandRequestHandler<T> OnSuccessAccepted(string path)
        {
            var resourceEndpoint = string.Format(path, _resourceId.ToString("N"));
            var operationEndpoint = $"operations/{_command.Details.Id:N}";
            _command.Details.Resource = resourceEndpoint;
            _responseFunc = x => _negotiator.WithStatusCode(202)
                .WithHeader("X-Resource", resourceEndpoint)
                .WithHeader("X-Operation", operationEndpoint);

            return this;
        }

        public CommandRequestHandler<T> OnSuccessRedirect(Func<T, string> func)
        {
            _responseFunc = x => _responseFormatter.AsRedirect(func(_command));

            return this;
        }

        public async Task<object> DispatchAsync()
        {
            object response = null;
            await _dispatcher.DispatchAsync(_command);

            if (_asyncResponseFunc != null)
            {
                response = await _asyncResponseFunc(_command);
            }
            else if (_responseFunc != null)
            {
                response = _responseFunc(_command);
            }

            return response;
        }
    }
}