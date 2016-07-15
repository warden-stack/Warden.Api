using System;
using System.Collections.Generic;
using System.Windows.Input;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warden.Api.Framework;
using Warden.Api.Framework.Handlers;
using Warden.Api.Infrastructure.Commands;

namespace Warden.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class ControllerBase : Controller
    {
        protected readonly string NotificationsKey = "Notifications";
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IMapper Mapper;

        protected ControllerBase(ICommandDispatcher commandDispatcher, IMapper mapper)
        {
            CommandDispatcher = commandDispatcher;
            Mapper = mapper;
        }

        protected RequestHandler<T> For<T>(T request) => new RequestHandler<T>(this, request);

        protected TModel MapTo<TModel>(object source)
            => (TModel) Mapper.Map(source, source.GetType(), typeof(TModel));

        //TODO: Log exception etc.
        public void HandleException(Exception exception)
        {
            throw exception;
        }
    }
}